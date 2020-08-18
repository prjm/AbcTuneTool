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
    public class AbcTokenizer : IDisposable {

        private AbcCharacter currentToken;
        private bool disposedValue;


        /// <summary>
        ///     create a new tokenizer
        /// </summary>
        /// <param name="reader">input</param>
        /// <param name="logger">logger</param>
        /// <param name="cache">string cache</param>
        public AbcTokenizer(TextReader reader, StringCache cache, Logger logger) {
            currentToken = AbcCharacters.Undefined;
            Reader = reader;
            Cache = cache;
            Logger = logger;
            buffer = new Queue<int>();
        }

        /// <summary>
        ///     read a char
        /// </summary>
        /// <returns></returns>
        public int ReadChar() {
            if (buffer.Count > 0)
                return buffer.Dequeue();

            return Reader.Read();
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

        private readonly Queue<int> buffer;

        /// <summary>
        ///     check if there are token left
        /// </summary>
        public bool HasToken
            => currentToken.Kind != AbcCharacterKind.Eof;

        /// <summary>
        ///     current token
        /// </summary>
        public AbcCharacter CurrentToken
            => currentToken;

        /// <summary>
        ///     read the next toke
        /// </summary>
        public void ReadNextToken() {
            if (!HasToken)
                return;

            var c = ReadChar();
            if (c < 0) {
                MarkEof();
                return;
            }

            var value = (char)c;
            if (value == '\\') {
                ReadMnemonic();
                return;
            }

            if (value == '&') {
                ReadEntity();
                return;
            }

            if (value == '$') {
                ReadFontSize();
                return;
            }

            if (value == '%') {
                ReadComment();
                return;
            }

            currentToken = new AbcCharacter(AbcCharacterKind.Char, value, Cache.ForChar(value));
        }

        private void ReadComment() {
            bool hasEof;
            var isLinebreakChar = false;
            var sb = new StringBuilder();
            var value = '\0';
            int c;

            sb.Append("%");

            do {
                c = ReadChar();
                hasEof = c < 0;
                if (!hasEof) {
                    value = (char)c;
                    sb.Append(value);
                    isLinebreakChar =
                        value == '\x000A' ||
                        value == '\x000B' ||
                        value == '\x000C' ||
                        value == '\x000D' ||
                        value == '\x0085' ||
                        value == '\x2028' ||
                        value == '\x2029';
                }

            } while (!hasEof && !isLinebreakChar);

            var isCr = value == '\x000D';
            if (!hasEof && isCr) {
                c = ReadChar();
                hasEof = c < 0;
                if (!hasEof) {
                    value = (char)c;
                    var isLf = value == '\x000A';
                    if (!isLf)
                        buffer.Enqueue(c);
                    else
                        sb.Append(value);
                }
            }
            currentToken = new AbcCharacter(AbcCharacterKind.Comment, '\0', Cache.ForStringBuilder(sb));
        }

        private void ReadFontSize() {
            var c = ReadChar();
            if (c < 0) {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', "$");
                Logger.Error(LogMessage.InvalidFontSize);
                return;
            }

            var value = Cache.ForChar((char)c);

            if (value == "$") {
                currentToken = new AbcCharacter(AbcCharacterKind.Dollar, '$', "$$");
                return;
            }

            if (!int.TryParse(value, out var _)) {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', Cache.ForChars('$', (char)c));
                Logger.Error(LogMessage.InvalidFontSize);
                return;
            }

            currentToken = new AbcCharacter(AbcCharacterKind.FontSize, value[0], Cache.ForChars('$', (char)c));
        }

        private void ReadEntity() {
            int c;
            char value;
            var sb = new StringBuilder();
            sb.Append("&");
            do {
                c = ReadChar();
                if (c < 0) {
                    currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', "&");
                    Logger.Error(LogMessage.InvalidEntity);
                    return;
                }
                value = (char)c;

                if (sb.Length == 1 && value == ' ') {
                    currentToken = new AbcCharacter(AbcCharacterKind.Ampersand, '&', "&");
                    buffer.Enqueue(' ');
                    return;
                }

                sb.Append(value);
            } while (c != ';');
            var entityName = Cache.ForStringBuilder(sb);

            if (string.Equals("&;", entityName, StringComparison.Ordinal)) {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', "&;");
                Logger.Error(LogMessage.InvalidEntity);
                return;
            }

            var charValue = Entities.Decode(entityName);

            if (charValue == '\0') {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', entityName);
                Logger.Error(LogMessage.UnknownEntity, entityName);
                return;
            }

            currentToken = new AbcCharacter(AbcCharacterKind.Entity, charValue, entityName);
        }

        private void MarkEof()
            => currentToken = AbcCharacters.Eof;

        private void ReadMnemonic() {
            var c = ReadChar();
            if (c < 0) {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', "\\");
                Logger.Error(LogMessage.InvalidMnemo1);
                return;
            }

            var decorator = (char)c;
            if (decorator == '\\') {
                currentToken = new AbcCharacter(AbcCharacterKind.Backslash, '\\', "\\\\"); ;
                return;
            }

            if (decorator == '&') {
                currentToken = new AbcCharacter(AbcCharacterKind.Ampersand, '&', "\\&"); ;
                return;
            }

            if (decorator == '%') {
                currentToken = new AbcCharacter(AbcCharacterKind.Percent, '%', "\\%");
                return;
            }

            var canBeUnicode = decorator == 'u' || decorator == 'U';

            c = ReadChar();
            if (c < 0) {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', Cache.ForChars('\\', decorator));
                Logger.Error(LogMessage.InvalidMnemo2);
                return;
            }

            var decoratedElement = (char)c;

            if (canBeUnicode && decoratedElement.IsHex()) {
                var len = 1;
                var hasEof = false;
                var chars = new char[8];
                chars[0] = decoratedElement;
                while (len < 8 && !hasEof && canBeUnicode) {
                    c = ReadChar();
                    if (c < 0) {
                        hasEof = true;
                    }
                    else {
                        canBeUnicode = ((char)c).IsHex();
                        if (canBeUnicode) {
                            chars[len] = (char)c;
                            len++;
                        }
                        else
                            buffer.Enqueue(c);
                    }
                }

                if (len == 8) {
                    var charString = new string(chars);
                    var charValue = int.Parse(charString, NumberStyles.HexNumber);
                    currentToken = new AbcCharacter(AbcCharacterKind.FixedUnicode4Byte, (char)charValue, "\\" + decorator + charString);
                    return;
                }

                if (len >= 5)
                    buffer.Enqueue(chars[4]);

                if (len >= 6)
                    buffer.Enqueue(chars[5]);

                if (len >= 7)
                    buffer.Enqueue(chars[6]);

                if (len >= 4) {
                    var charString = new string(chars, 0, 4);
                    var charValue = int.Parse(charString, NumberStyles.HexNumber);
                    currentToken = new AbcCharacter(AbcCharacterKind.FixedUnicody2Byte, (char)charValue, "\\" + decorator + charString);
                    return;
                }

                if (len >= 2)
                    buffer.Enqueue(chars[1]);

                if (len >= 3)
                    buffer.Enqueue(chars[2]);
            }

            var value = Mnemonics.Decode(decorator, decoratedElement);

            if (value == '\0') {
                currentToken = new AbcCharacter(AbcCharacterKind.Char, '\0', Cache.ForChars('\\', decorator, decoratedElement));
                Logger.Error(LogMessage.UnknownMnemo, string.Concat('\\', decorator, decoratedElement));
                return;
            }

            currentToken = new AbcCharacter(AbcCharacterKind.Mnenomic, value, Cache.ForChars('\\', decorator, decoratedElement));
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
