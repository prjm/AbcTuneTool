using System;
using System.Collections.Generic;
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
        public AbcParser(BufferedAbcTokenizer tokenizer)
            => Tokenizer = tokenizer;

        /// <summary>
        ///     tokenizer
        /// </summary>
        public BufferedAbcTokenizer Tokenizer { get; }

        private AbcCharacterReference CurrentToken
            => Tokenizer.Lookahead(0);

        /// <summary>
        ///     parse an information field
        /// </summary>
        /// <returns></returns>
        public InformationField? ParseInformationField() {
            if (Matches(AbcCharacterKind.InformationFieldHeader)) {
                var field = CurrentToken;
                NextToken();
                var values = new List<AbcCharacterReference>();
                while (!Matches(AbcCharacterKind.Eof, AbcCharacterKind.Linebreak)) {
                    values.Add(CurrentToken);
                    NextToken();
                }

                return new InformationField(field);
            }

            return default;
        }

        private bool Matches(AbcCharacterKind kind1, AbcCharacterKind kind2)
            => CurrentToken.AbcChar.Kind == kind1 || CurrentToken.AbcChar.Kind == kind2;

        private bool Matches(AbcCharacterKind kind)
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
