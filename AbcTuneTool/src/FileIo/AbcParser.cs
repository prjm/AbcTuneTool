using System;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     parser for ABC files
    /// </summary>
    public class AbcParser : IDisposable {
        bool disposedValue;

        /// <summary>
        ///     create a new ABC file parser
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <param name="listPools">list pools</param>
        public AbcParser(BufferedAbcTokenizer tokenizer, ListPools listPools) {
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

        private AbcCharacterReference CurrentToken
            => Tokenizer.Lookahead(0);

        /// <summary>
        ///     parse an information field
        /// </summary>
        /// <returns></returns>
        public InformationField? ParseInformationField() {
            if (Matches(TokenKind.InformationFieldHeader)) {
                var field = CurrentToken;
                NextToken();

                using var values = ListPools.GetList();
                while (!Matches(TokenKind.Eof, TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                if (Matches(TokenKind.Linebreak))
                    values.Add(GetCurrentTokenAndFetchNext());

                return new InformationField(field, values.ToImmutableArray<AbcCharacterReference>());
            }

            return default;
        }

        private AbcCharacterReference GetCurrentTokenAndFetchNext() {
            var result = CurrentToken;
            NextToken();
            return result;
        }

        /// <summary>
        ///     parse a set of information fields
        /// </summary>
        /// <returns></returns>
        public InformationFields ParseInformationFields() {
            using var values = ListPools.GetList();
            while (Matches(TokenKind.InformationFieldHeader)) {
                var field = ParseInformationField();
                if (!(field is null))
                    values.Add(field);
            }

            return new InformationFields(values.ToImmutableArray<InformationField>());
        }

        public OtherLines ParseLinesBeforeHeader() {
            using var values = ListPools.GetList();

            while (!Matches(TokenKind.Eof, TokenKind.InformationFieldHeader)) {
                values.Add(GetCurrentTokenAndFetchNext());
            }
            return new OtherLines(values.ToImmutableArray<AbcCharacterReference>());
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

        /// <summary>
        ///     parse a tune booke
        /// </summary>
        /// <returns></returns>
        public TuneBook ParseTuneBook() {
            var hasFileHeader = false;
            var fileHeader = InformationFields.Empty;
            using var list = ListPools.ObjectLists.GetItem();

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

            return new TuneBook(fileHeader, list.ToImmutableArray<Tune>());
        }

        private bool Matches(TokenKind kind1, TokenKind kind2)
            => CurrentToken.AbcChar.Kind == kind1 || CurrentToken.AbcChar.Kind == kind2;

        private bool Matches(TokenKind kind)
            => CurrentToken.AbcChar.Kind == kind;

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
