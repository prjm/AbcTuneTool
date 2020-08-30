using System;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

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
        }

        /// <summary>
        ///     tokenizer
        /// </summary>
        public BufferedAbcTokenizer Tokenizer { get; }

        /// <summary>
        ///     list pools
        /// </summary>
        public ListPools ListPools { get; }

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
                return kind switch
                {
                    InformationFieldKind.Instruction
                        => new InstructionField(header, new Terminal(values), cache, pool),

                    _ => new InformationField(header, new Terminal(values), kind),
                };
            }

            return default;
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
