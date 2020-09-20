using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     tokenizer for ABC files
    /// </summary>
    public class Tokenizer : IDisposable {

        private bool disposedValue;
        private readonly HashSet<(char, char)> skippedMnemos
            = new HashSet<(char, char)>();

        /// <summary>
        ///     create a new tokenizer
        /// </summary>
        /// <param name="reader">input</param>
        /// <param name="logger">logger</param>
        /// <param name="cache">string cache</param>
        /// <param name="pool">string builder pool</param>
        public Tokenizer(TextReader reader, StringCache cache, StringBuilderPool pool, Logger logger) {
            Reader = reader;
            Cache = cache;
            Logger = logger;
            StringBuilderPool = pool;
            buffer = new Queue<char>();
            currentToken = new Token(string.Empty, string.Empty, TokenKind.Undefined);
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
        ///     string builder pool
        /// </summary>
        public StringBuilderPool StringBuilderPool { get; }

        /// <summary>
        ///     unread, buffered chars
        /// </summary>
        private readonly Queue<char> buffer;

        /// <summary>
        ///     check if there are token left
        /// </summary>
        public bool HasToken
            => currentToken.Kind != TokenKind.Eof;

        /// <summary>
        ///     current token
        /// </summary>
        public ref Token CurrentToken
            => ref currentToken;

        private Token currentToken;

        /// <summary>
        ///     get a token
        /// </summary>
        /// <param name="value"></param>
        /// <param name="originalValue"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        private void SetToken(string value, string originalValue, in TokenKind kind)
            => currentToken = new Token(value, originalValue, kind);

        // flags..
        private bool isEmptyLine = true;
        private InformationFieldKind isInInfoField = InformationFieldKind.Undefined;

        /// <summary>
        ///     read the next token
        /// </summary>
        public bool ReadNextToken() {
            if (!HasToken)
                return false;

            if (!ReadChar(out var value))
                return MarkEof();

            if (value.IsLinebreak())
                return ReadLinebreak(value, default);

            if (isEmptyLine && value.IsWhitespace() && TryReadEmptyLine(value))
                return true;

            if (isEmptyLine && value.IsAsciiLetter() && ReadChar(out var seperator)) {
                if (seperator == ':')
                    return ReadInformationFieldHeader(value, seperator);

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

        private bool ReadInformationFieldHeader(char value, char seperator) {
            isEmptyLine = false;
            var field = Cache.ForChars(value, seperator);
            SetToken(field, field, TokenKind.InformationFieldHeader);
            isInInfoField = InformationField.GetKindFor(value);
            return true;
        }

        private bool TryReadEmptyLine(char value) {
            using var sb = StringBuilderPool.Rent();
            sb.Item.Append(value);

            while (ReadChar(out value)) {

                if (value.IsLinebreak()) {
                    ReadLinebreak(value, sb);
                    return true;
                }

                sb.Item.Append(value);

                if (!value.IsWhitespace()) {
                    for (var i = 1; i < sb.Item.Length; i++)
                        buffer.Enqueue(sb.Item[i]);
                    return false;
                }
            }

            ReadLinebreak(value, sb);
            return true;
        }

        private bool ReadDefaultInfoField(char value) {
            using var sb = StringBuilderPool.Rent();
            sb.Item.Append(value);

            while (ReadChar(out var nextValue)) {

                if (nextValue.IsLinebreak()) {
                    buffer.Enqueue(nextValue);
                    break;
                }
                else {
                    sb.Item.Append(nextValue);
                }
            }

            var headerData = Cache.ForStringBuilder(sb.Item);
            SetToken(headerData, headerData, TokenKind.Char);
            return true;
        }

        private bool ReadWhitespaceSeparatedInfoField(char value) {
            using var sb = StringBuilderPool.Rent();
            var wasWhitespace = value.IsWhitespace();

            sb.Item.Append(value);

            while (ReadChar(out var nextValue)) {

                var isWhitespace = nextValue.IsWhitespace();

                if (nextValue.IsLinebreak() || isWhitespace != wasWhitespace) {
                    buffer.Enqueue(nextValue);
                    break;
                }
                else {
                    sb.Item.Append(nextValue);
                }
            }

            var headerData = Cache.ForStringBuilder(sb.Item);
            SetToken(headerData, headerData, TokenKind.Char);
            return true;
        }

        private bool ReadSemicolonSeparatedInfoField(char value) {
            using var sb = StringBuilderPool.Rent();
            var wasSemicolon = value == ';';

            sb.Item.Append(value);

            while (ReadChar(out var nextValue)) {

                var isWhitespace = nextValue.IsWhitespace();
                var isSemicolon = nextValue == ';';

                if (nextValue.IsLinebreak() || wasSemicolon && !isWhitespace || isSemicolon) {
                    buffer.Enqueue(nextValue);
                    break;
                }
                else {
                    sb.Item.Append(nextValue);
                }
            }

            var headerData = Cache.ForStringBuilder(sb.Item);
            SetToken(headerData, headerData, TokenKind.Char);
            return true;
        }

        private bool ReadLinebreak(char value, ObjectPoolItem<StringBuilder>? sb) {
            var kind = isEmptyLine ? TokenKind.EmptyLine : TokenKind.Linebreak;
            string cr() => sb == default ? Cache.ForChar(value) : Cache.ForStringBuilder(sb, value);
            string crlf(char lf) => sb == default ? Cache.ForChars(value, lf) : Cache.ForStringBuilder(sb, value, lf);

            if (value.IsCr() && ReadChar(out var lf)) {
                if (lf.IsLf())
                    SetToken(string.Empty, crlf(lf), kind);
                else {
                    buffer.Enqueue(lf);
                    lf = '\0';
                    SetToken(string.Empty, cr(), kind);
                }
            }
            else {
                lf = '\0';
                SetToken(string.Empty, cr(), kind);
            }

            if (isInInfoField != InformationFieldKind.Undefined && sb == default) {
                if (ReadChar(out var letter)) {
                    if (ReadChar(out var colon)) {
                        if (letter == '+' && colon == ':') {
                            if (lf != '\0')
                                SetToken(string.Empty, Cache.ForChars(value, lf, letter, colon), TokenKind.HeaderContinuation);
                            else
                                SetToken(string.Empty, Cache.ForChars(value, letter, colon), TokenKind.HeaderContinuation);

                            return true;
                        }
                        isInInfoField = InformationFieldKind.Undefined;
                        buffer.Enqueue(letter);
                        buffer.Enqueue(colon);
                        isEmptyLine = true;
                        return true;
                    }

                    isInInfoField = InformationFieldKind.Undefined;
                    buffer.Enqueue(letter);
                    isEmptyLine = true;
                    return true;
                }
            }

            isEmptyLine = true;
            return true;
        }

        /// <summary>
        ///     read a default char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ReadDefault(char value) {
            if (isInInfoField != InformationFieldKind.Undefined) {
                return isInInfoField.GetContentType() switch
                {
                    InformationFieldContent.Instruction => ReadWhitespaceSeparatedInfoField(value),
                    InformationFieldContent.Key => ReadWhitespaceSeparatedInfoField(value),
                    InformationFieldContent.NoteLength => ReadWhitespaceSeparatedInfoField(value),
                    InformationFieldContent.Meter => ReadWhitespaceSeparatedInfoField(value),
                    InformationFieldContent.Macro => ReadWhitespaceSeparatedInfoField(value),
                    InformationFieldContent.Origin => ReadSemicolonSeparatedInfoField(value),
                    InformationFieldContent.Tempo => ReadWhitespaceSeparatedInfoField(value),
                    _ => ReadDefaultInfoField(value),
                };
            }

            var tokenValue = Cache.ForChar(value);
            SetToken(tokenValue, tokenValue, TokenKind.Char);
            return true;
        }

        /// <summary>
        ///     read a comment value
        /// </summary>
        /// <returns></returns>
        private bool ReadComment() {
            char value;
            bool isLinebreak;

            using var sb = StringBuilderPool.Rent();
            sb.Item.Append("%");

            do {
                if (!ReadChar(out value))
                    break;
                isLinebreak = value.IsLinebreak();
                sb.Item.Append(value);
            } while (!isLinebreak);

            if (value.IsCr()) {
                if (ReadChar(out value))
                    if (!value.IsLf())
                        buffer.Enqueue(value);
                    else
                        sb.Item.Append(value);
            }

            SetToken(string.Empty, Cache.ForStringBuilder(sb.Item), TokenKind.Comment);
            return true;
        }

        private bool ReadFontSize() {
            if (!ReadChar(out var c)) {
                SetToken(string.Empty, "$", TokenKind.Char);
                Logger.Error(LogMessage.InvalidFontSize);
                return false;
            }

            var value = Cache.ForChar(c);

            if (value == "$") {
                SetToken("$", "$$", TokenKind.Dollar);
                return true;
            }

            if (!int.TryParse(value, out var _)) {
                SetToken(string.Empty, Cache.ForChars('$', (char)c), TokenKind.Char);
                Logger.Error(LogMessage.InvalidFontSize2, value);
                return false;
            }

            SetToken(value, Cache.ForChars('$', (char)c), TokenKind.FontSize);
            return true;
        }

        private bool ReadEntity() {
            char value;
            using var sb = StringBuilderPool.Rent();
            sb.Item.Append("&");
            do {
                if (!ReadChar(out value)) {
                    SetToken(string.Empty, "&", TokenKind.Char);
                    Logger.Error(LogMessage.InvalidEntity1);
                    return false;
                }

                if (sb.Item.Length == 1 && value == ' ') {
                    SetToken("&", "&", TokenKind.Ampersand);
                    buffer.Enqueue(' ');
                    return false;
                }

                if (value.IsLinebreak() || value != ';' && !value.IsAsciiLetter()) {
                    SetToken(string.Empty, Cache.ForStringBuilder(sb.Item), TokenKind.Entity);
                    Logger.Error(LogMessage.InvalidEntity2, Cache.ForStringBuilder(sb.Item));
                    buffer.Enqueue(value);
                    return false;
                }

                sb.Item.Append(value);

            } while (value != ';');
            var entityName = Cache.ForStringBuilder(sb.Item);

            if (string.Equals("&;", entityName, StringComparison.Ordinal)) {
                SetToken(string.Empty, "&;", TokenKind.Char);
                Logger.Error(LogMessage.InvalidEntity1);
                return false;
            }

            var charValue = Entities.Decode(entityName);

            if (string.IsNullOrEmpty(charValue)) {
                SetToken(string.Empty, entityName, TokenKind.Char);
                Logger.Error(LogMessage.UnknownEntity, entityName);
                return false;
            }

            SetToken(charValue, entityName, TokenKind.Entity);
            return true;
        }

        private bool MarkEof() {
            CurrentToken = new Token(string.Empty, string.Empty, TokenKind.Eof);
            return false;
        }

        private bool ReadMnemonic() {
            if (!ReadChar(out var decorator)) {
                SetToken(string.Empty, "\\", TokenKind.Char);
                Logger.Error(LogMessage.InvalidMnemo1);
                return false;
            }

            if (decorator.IsLinebreak() || decorator == ' ' || char.IsWhiteSpace(decorator)) {
                using var sb = StringBuilderPool.Rent();
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

                SetToken(string.Empty, Cache.ForStringBuilder(sb.Item), TokenKind.LineContinuation);
                return true;
            }

            if (decorator == '\\') {
                SetToken("\\", "\\\\", TokenKind.Backslash);
                return true;
            }

            if (decorator == '&') {
                SetToken("&", "\\&", TokenKind.Ampersand);
                return true;
            }

            if (decorator == '%') {
                SetToken("%", "\\%", TokenKind.Percent);
                return true;
            }

            var canBeUnicode = decorator == 'u' || decorator == 'U';

            if (!ReadChar(out var decoratedElement)) {
                SetToken(string.Empty, Cache.ForChars('\\', decorator), TokenKind.Char);
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
                    SetToken(char.ConvertFromUtf32(charValue), "\\" + decorator + charString, TokenKind.FixedUnicode4Byte);
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
                    SetToken(char.ConvertFromUtf32(charValue), "\\" + decorator + charString, TokenKind.FixedUnicody2Byte);
                    return true;
                }

                if (len >= 2)
                    buffer.Enqueue(chars[1]);

                if (len >= 3)
                    buffer.Enqueue(chars[2]);
            }

            var mnemo = Mnemonics.Decode(decorator, decoratedElement);

            if (string.IsNullOrEmpty(mnemo)) {
                SetToken(string.Empty, Cache.ForChars('\\', decorator, decoratedElement), TokenKind.Char);

                if (!skippedMnemos.Contains((decorator, decoratedElement))) {
                    skippedMnemos.Add((decorator, decoratedElement));
                    Logger.Error(LogMessage.UnknownMnemo, Cache.ForChars('\\', decorator, decoratedElement));
                }

                return false;
            }

            SetToken(mnemo, Cache.ForChars('\\', decorator, decoratedElement), TokenKind.Mnenomic);
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
