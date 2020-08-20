using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     tokenizer for ABC files
    /// </summary>
    public class AbcTokenizer : IDisposable {

        private bool disposedValue;


        /// <summary>
        ///     create a new tokenizer
        /// </summary>
        /// <param name="reader">input</param>
        /// <param name="logger">logger</param>
        /// <param name="cache">string cache</param>
        /// <param name="charCache">char cache</param>
        /// <param name="pool">string builder pool</param>
        public AbcTokenizer(TextReader reader, StringCache cache, AbcCharacterCache charCache, StringBuilderPool pool, Logger logger) {
            Reader = reader;
            Cache = cache;
            Logger = logger;
            CharCache = charCache;
            StringBuilderPool = pool;
            buffer = new Queue<char>();
            CurrentToken = CharCache.FromCache(string.Empty, string.Empty, AbcCharacterKind.Undefined);
        }

        /// <summary>
        ///     read a char
        /// </summary>
        /// <returns></returns>
        public bool ReadChar(out char c) {
            if (buffer.Count > 0) {
                c = buffer.Dequeue();
                return true;
            }

            var value = Reader.Read();

            if (value < 0) {
                c = '\0';
                return false;
            }

            c = (char)value;
            return true;
        }

        /// <summary>
        ///     stream reader
        /// </summary>
        public TextReader Reader { get; }

        /// <summary>
        ///     string cache
        /// </summary>
        public StringCache Cache { get; }

        /// <summary>
        ///     logger
        /// </summary>
        public Logger Logger { get; }

        /// <summary>
        ///     char cache
        /// </summary>
        public AbcCharacterCache CharCache { get; }

        /// <summary>
        ///     string builder pool
        /// </summary>
        public StringBuilderPool StringBuilderPool { get; }

        private readonly Queue<char> buffer;

        /// <summary>
        ///     check if there are token left
        /// </summary>
        public bool HasToken
            => CurrentToken.AbcChar.Kind != AbcCharacterKind.Eof;

        /// <summary>
        ///     current token
        /// </summary>
        public AbcCharacterReference CurrentToken { get; private set; }

        /// <summary>
        ///     get a token
        /// </summary>
        /// <param name="value"></param>
        /// <param name="originalValue"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        private AbcCharacterReference GetToken(string value, string originalValue, AbcCharacterKind kind)
            => CharCache.FromCache(value, originalValue, kind);

        private bool isEmptyLine = true;

        /// <summary>
        ///     read the next toke
        /// </summary>
        public bool ReadNextToken() {
            if (!HasToken)
                return false;

            if (!ReadChar(out var value))
                return MarkEof();

            if (value.IsLinebreak())
                return ReadLinebreak(value);

            if (isEmptyLine && value.MarksInfoField() && ReadChar(out var seperator)) {
                if (seperator == ':') {
                    var field = Cache.ForChars(value, seperator);
                    CurrentToken = GetToken(field, field, AbcCharacterKind.InformationFieldHeader);
                    return true;
                }
                buffer.Enqueue(seperator);
            }

            isEmptyLine = false;

            return value switch
            {
                '\\' => ReadMnemonic(),
                '&' => ReadEntity(),
                '$' => ReadFontSize(),
                '%' => ReadComment(),
                _ => ReadDefault(value)
            };
        }

        private bool ReadLinebreak(char value) {
            var kind = isEmptyLine ? AbcCharacterKind.EmptyLine : AbcCharacterKind.Linebreak;

            if (ReadChar(out var lf)) {
                if (lf.IsLf())
                    CurrentToken = GetToken(string.Empty, Cache.ForChars(value, lf), kind);
                else {
                    buffer.Enqueue(lf);
                    CurrentToken = GetToken(string.Empty, Cache.ForChar(value), kind);
                }
            }
            else
                CurrentToken = GetToken(string.Empty, Cache.ForChar(value), kind);

            return true;
        }

        /// <summary>
        ///     read a default char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ReadDefault(char value) {
            var tokenValue = Cache.ForChar(value);
            CurrentToken = GetToken(tokenValue, tokenValue, AbcCharacterKind.Char);
            return true;
        }

        /// <summary>
        ///     read a comment value
        /// </summary>
        /// <returns></returns>
        private bool ReadComment() {
            bool isLinebreakChar;
            char value;

            using var sb = StringBuilderPool.GetItem();
            sb.Item.Append("%");

            do {
                if (!ReadChar(out value))
                    break;
                sb.Item.Append(value);
                isLinebreakChar = value.IsLinebreak();
            } while (!isLinebreakChar);

            if (value.IsCr()) {
                if (ReadChar(out value))
                    if (!value.IsLf())
                        buffer.Enqueue(value);
                    else
                        sb.Item.Append(value);
            }

            CurrentToken = GetToken(string.Empty, Cache.ForStringBuilder(sb.Item), AbcCharacterKind.Comment);
            return true;
        }

        private bool ReadFontSize() {
            if (!ReadChar(out var c)) {
                CurrentToken = GetToken(string.Empty, "$", AbcCharacterKind.Char);
                Logger.Error(LogMessage.InvalidFontSize);
                return false;
            }

            var value = Cache.ForChar(c);

            if (value == "$") {
                CurrentToken = GetToken("$", "$$", AbcCharacterKind.Dollar);
                return true;
            }

            if (!int.TryParse(value, out var _)) {
                CurrentToken = GetToken(string.Empty, Cache.ForChars('$', (char)c), AbcCharacterKind.Char);
                Logger.Error(LogMessage.InvalidFontSize);
                return false;
            }

            CurrentToken = GetToken(value, Cache.ForChars('$', (char)c), AbcCharacterKind.FontSize);
            return true;
        }

        private bool ReadEntity() {
            char value;
            using var sb = StringBuilderPool.GetItem();
            sb.Item.Append("&");
            do {
                if (!ReadChar(out value)) {
                    CurrentToken = GetToken(string.Empty, "&", AbcCharacterKind.Char);
                    Logger.Error(LogMessage.InvalidEntity);
                    return false;
                }

                if (sb.Item.Length == 1 && value == ' ') {
                    CurrentToken = GetToken("&", "&", AbcCharacterKind.Ampersand);
                    buffer.Enqueue(' ');
                    return false;
                }

                sb.Item.Append(value);
            } while (value != ';');
            var entityName = Cache.ForStringBuilder(sb.Item);

            if (string.Equals("&;", entityName, StringComparison.Ordinal)) {
                CurrentToken = GetToken(string.Empty, "&;", AbcCharacterKind.Char);
                Logger.Error(LogMessage.InvalidEntity);
                return false;
            }

            var charValue = Entities.Decode(entityName);

            if (string.IsNullOrEmpty(charValue)) {
                CurrentToken = GetToken(string.Empty, entityName, AbcCharacterKind.Char);
                Logger.Error(LogMessage.UnknownEntity, entityName);
                return false;
            }

            CurrentToken = GetToken(charValue, entityName, AbcCharacterKind.Entity);
            return true;
        }

        private bool MarkEof() {
            CurrentToken = CharCache.FromCache(string.Empty, string.Empty, AbcCharacterKind.Eof);
            return false;
        }

        private bool ReadMnemonic() {
            if (!ReadChar(out var decorator)) {
                CurrentToken = GetToken(string.Empty, "\\", AbcCharacterKind.Char);
                Logger.Error(LogMessage.InvalidMnemo1);
                return false;
            }

            if (decorator.IsLinebreak() || decorator == ' ' || char.IsWhiteSpace(decorator)) {
                using var sb = StringBuilderPool.GetItem();
                sb.Item.Append("\\");
                sb.Item.Append(decorator);

                while (!decorator.IsLinebreak()) {
                    if (ReadChar(out decorator))
                        sb.Item.Append(decorator);
                    else
                        break;
                }

                if (ReadChar(out var lf)) {
                    if (lf.IsLf())
                        sb.Item.Append(lf);
                    else
                        buffer.Enqueue(lf);
                }

                CurrentToken = GetToken(string.Empty, Cache.ForStringBuilder(sb.Item), AbcCharacterKind.LineContinuation);
                return true;
            }

            if (decorator == '\\') {
                CurrentToken = GetToken("\\", "\\\\", AbcCharacterKind.Backslash);
                return true;
            }

            if (decorator == '&') {
                CurrentToken = GetToken("&", "\\&", AbcCharacterKind.Ampersand);
                return true;
            }

            if (decorator == '%') {
                CurrentToken = GetToken("%", "\\%", AbcCharacterKind.Percent);
                return true;
            }

            var canBeUnicode = decorator == 'u' || decorator == 'U';

            if (!ReadChar(out var decoratedElement)) {
                CurrentToken = GetToken(string.Empty, Cache.ForChars('\\', decorator), AbcCharacterKind.Char);
                Logger.Error(LogMessage.InvalidMnemo2);
                return false;
            }

            if (canBeUnicode && decoratedElement.IsHex()) {
                var len = 1;
                Span<char> chars = stackalloc char[8];
                chars[0] = decoratedElement;
                while (len < 8 && canBeUnicode && ReadChar(out var value)) {
                    canBeUnicode = value.IsHex();
                    if (canBeUnicode) {
                        chars[len] = value;
                        len++;
                    }
                    else
                        buffer.Enqueue(value);
                };

                if (len == 8) {
                    var charString = chars.ToString();
                    var charValue = int.Parse(charString, NumberStyles.HexNumber);
                    CurrentToken = GetToken(char.ConvertFromUtf32(charValue), "\\" + decorator + charString, AbcCharacterKind.FixedUnicode4Byte);
                    return true;
                }

                if (len >= 5)
                    buffer.Enqueue(chars[4]);

                if (len >= 6)
                    buffer.Enqueue(chars[5]);

                if (len >= 7)
                    buffer.Enqueue(chars[6]);

                if (len >= 4) {
                    var charString = chars.Slice(0, 4).ToString();
                    var charValue = int.Parse(charString, NumberStyles.HexNumber);
                    CurrentToken = GetToken(char.ConvertFromUtf32(charValue), "\\" + decorator + charString, AbcCharacterKind.FixedUnicody2Byte);
                    return true;
                }

                if (len >= 2)
                    buffer.Enqueue(chars[1]);

                if (len >= 3)
                    buffer.Enqueue(chars[2]);
            }

            var mnemo = Mnemonics.Decode(decorator, decoratedElement);

            if (string.IsNullOrEmpty(mnemo)) {
                CurrentToken = GetToken(string.Empty, Cache.ForChars('\\', decorator, decoratedElement), AbcCharacterKind.Char);
                Logger.Error(LogMessage.UnknownMnemo, Cache.ForChars('\\', decorator, decoratedElement));
                return false;
            }

            CurrentToken = GetToken(mnemo, Cache.ForChars('\\', decorator, decoratedElement), AbcCharacterKind.Mnenomic);
            return true;
        }

        /// <summary>
        ///     dispose this tokenizer
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Reader.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        ///     dispose this tokenizer
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
