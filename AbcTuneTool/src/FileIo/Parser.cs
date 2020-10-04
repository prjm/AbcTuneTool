using System;
using System.Collections.Immutable;
using AbcTuneTool.Common;
using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.Symbolic;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     parser for ABC files
    /// </summary>
    public class Parser : IDisposable {
        bool disposedValue;

        /// <summary>
        ///     create a new ABC file parser
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <param name="listPools">list pools</param>
        public Parser(BufferedAbcTokenizer tokenizer, ListPools listPools) {
            Tokenizer = tokenizer;
            ListPools = listPools;
            Symbols = new DecorationRegistry();
            Shortcuts = new SymbolShortcuts();
        }

        /// <summary>
        ///     tokenizer
        /// </summary>
        public BufferedAbcTokenizer Tokenizer { get; }

        /// <summary>
        ///     list pools
        /// </summary>
        public ListPools ListPools { get; }

        /// <summary>
        ///     symbols
        /// </summary>
        public DecorationRegistry Symbols { get; }

        /// <summary>
        ///     shortcuts
        /// </summary>
        public SymbolShortcuts Shortcuts { get; }

        private Token CurrentToken
            => Tokenizer.Lookahead(0);

        /// <summary>
        ///     parse an information field
        /// </summary>
        /// <returns></returns>
        public InformationField? ParseInformationField() {
            if (Matches(TokenKind.InformationFieldHeader)) {
                var field = CurrentToken;
                NextToken();

                using var values = ListPools.GetTokenList();
                while (!Matches(TokenKind.Eof, TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                if (Matches(TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                var header = new Terminal(field);
                var kind = InformationField.GetKindFor(header.FirstChar);
                var cache = Tokenizer.Tokenizer.Cache;
                var pool = Tokenizer.Tokenizer.StringBuilderPool;
                var fieldValues = new Terminal(values);
                return kind switch
                {
                    InformationFieldKind.Instruction
                        => new InstructionField(header, fieldValues, cache, pool),

                    InformationFieldKind.Key
                        => new KeyField(header, fieldValues),

                    InformationFieldKind.UnitNoteLength
                        => new LengthField(header, fieldValues),

                    InformationFieldKind.Meter
                        => new MeterField(header, fieldValues),

                    InformationFieldKind.Macro
                        => new MacroField(header, fieldValues),

                    InformationFieldKind.Parts
                        => new PartsField(header, fieldValues),

                    InformationFieldKind.Tempo
                        => new TempoField(header, fieldValues),

                    InformationFieldKind.SymbolLine
                        => new SymbolLineField(header, fieldValues, ParseTuneElements(fieldValues)),

                    InformationFieldKind.UserDefined
                        => ParseUserDefinedField(header, fieldValues),

                    _ => new InformationField(header, new Terminal(values), kind),
                };
            }

            return default;
        }

        private UserDefinedField ParseUserDefinedField(Terminal header, Terminal fieldValues) {
            var alias = fieldValues.GetValueAfterWhitespace(0, out var index);
            _ = fieldValues.GetValueAfterWhitespace(1 + index, out index);
            var symbol = fieldValues.GetValueAfterWhitespace(1 + index, out _);
            return new UserDefinedField(header, fieldValues, alias, ParseTuneElement(symbol));
        }

        private ImmutableArray<TuneElement> ParseTuneElements(Terminal fieldValues) {
            using var list = ListPools.ObjectLists.Rent();

            for (var i = 0; i < fieldValues.Length; i++) {

                var text = fieldValues[i];

                if (text.Length < 1) continue;

                var element = ParseTuneElement(text);
                if (element != default)
                    list.Add(element);
            }

            return list.ToImmutableArray<TuneElement>();
        }

        private TuneElement? ParseTuneElement(string text) {
            var element = default(TuneElement);

            if (text[0] == '"' && text.Length > 3) {
                element = new Annotation(text[1].AsPosition(), text[2..^1]);
            }

            else if (text.Length == 1 && text[0].IsDecorationShortcut() && Shortcuts.Shortcuts.TryGetValue(text[0], out var symbol2)) {
                element = new TuneSymbol(symbol2);
            }

            else if (text[0] == '!' && text[^1] == '!' && text.Length > 2) {
                var symbols = text[1..^1];
                if (Symbols.Symbols.TryGetValue(symbols, out var symbol))
                    element = new TuneSymbol(symbol);
                else if (string.Equals(symbols, KnownStrings.None, StringComparison.CurrentCultureIgnoreCase))
                    element = new UndefinedTuneSymbol();
                else if (string.Equals(symbols, KnownStrings.Nil, StringComparison.CurrentCultureIgnoreCase))
                    element = new UndefinedTuneSymbol();
            }

            else if (text[0].IsNoteLetter()) {
                var firstNote = text[0];
                var accidental = text[1].AsAccidental(false, Accidental.Undefined);
                var slashIndex = text.IndexOf('/');
                var bassNote = '\0';
                var bassAccidental = Accidental.Undefined;
                var type = string.Empty;
                var startIndex = accidental != Accidental.Undefined ? 2 : 1;

                if (slashIndex >= 0 && slashIndex + 1 < text.Length) {
                    if (slashIndex > startIndex & text.Length - startIndex - slashIndex > 0)
                        type = text.Substring(startIndex, text.Length - startIndex - slashIndex - 1);

                    bassNote = text[slashIndex + 1];
                    if (slashIndex + 2 < text.Length)
                        bassAccidental = text[slashIndex + 2].AsAccidental();
                }
                else {
                    type = text.Substring(startIndex);
                }

                element = new ChordSymbol(firstNote, accidental, type, bassNote, bassAccidental);
            }

            return element;
        }

        private Token GetCurrentTokenAndFetchNext() {
            var result = CurrentToken;
            NextToken();
            return result;
        }

        /// <summary>
        ///     parse a set of information fields
        /// </summary>
        /// <returns></returns>
        public InformationFields ParseInformationFields() {
            using var values = ListPools.GetObjectList();
            while (Matches(TokenKind.InformationFieldHeader)) {
                var field = ParseInformationField();
                if (!(field is null))
                    values.Add(field);
            }

            return new InformationFields(values.ToImmutableArray<InformationField>());
        }

        /// <summary>
        ///     parse the lines before the header
        /// </summary>
        /// <returns></returns>
        public OtherLines ParseLinesBeforeHeader() {
            using var values = ListPools.GetObjectList();

            while (!Matches(TokenKind.Eof, TokenKind.InformationFieldHeader)) {
                values.Add(GetCurrentTokenAndFetchNext());
            }
            return new OtherLines(values.ToImmutableArray<Token>());
        }

        /// <summary>
        ///     parse a single tune
        /// </summary>
        /// <returns></returns>
        public Tune ParseTune() {
            var header = InformationFields.Empty;
            var otherLines = ParseLinesBeforeHeader();

            if (Matches(TokenKind.InformationFieldHeader)) {
                header = ParseInformationFields();
            }

            while (!Matches(TokenKind.Eof, TokenKind.EmptyLine)) {
                Tokenizer.NextToken();
            }

            return new Tune(otherLines, header);
        }

        internal string ExtractVersion(Token token) {
            var dashIndex = token.OriginalValue.IndexOf("-") + 1;
            if (dashIndex != KnownStrings.VersionComment.Length)
                return KnownStrings.UndefinedVersion;
            return token.OriginalValue.Substring(dashIndex);
        }

        /// <summary>
        ///     parse a tune book
        /// </summary>
        /// <returns></returns>
        public TuneBook ParseTuneBook() {
            var hasFileHeader = false;
            var version = KnownStrings.UndefinedVersion;
            var fileHeader = InformationFields.Empty;
            using var list = ListPools.ObjectLists.Rent();

            if (Matches(TokenKind.Comment) && CurrentToken.OriginalValue.StartsWith(KnownStrings.VersionComment)) {
                version = ExtractVersion(CurrentToken);
            }

            while (!Matches(TokenKind.Eof)) {

                var otherLines = ParseLinesBeforeHeader();

                if (Matches(TokenKind.InformationFieldHeader)) {
                    if (!hasFileHeader) {
                        fileHeader = ParseInformationFields();
                        hasFileHeader = true;
                        continue;
                    }
                    list.Add(ParseTune());
                }

            }

            return new TuneBook(version, fileHeader, list.ToImmutableArray<Tune>());
        }

        private bool Matches(TokenKind kind1, TokenKind kind2)
            => CurrentToken.Kind == kind1 || CurrentToken.Kind == kind2;

        private bool Matches(TokenKind kind)
            => CurrentToken.Kind == kind;

        private void NextToken()
            => Tokenizer.NextToken();

        /// <summary>
        ///     dispose this parser
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Tokenizer.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        ///     dispose this parser
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
