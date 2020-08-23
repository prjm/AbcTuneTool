using System;
using System.Collections.Generic;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     buffered tokenizer with lookahead
    /// </summary>
    public class BufferedAbcTokenizer : IDisposable {


        private readonly Queue<AbcCharacterReference> tokens =
            new Queue<AbcCharacterReference>();

        bool disposedValue;

        /// <summary>
        ///     create a new buffered tokenizer
        /// </summary>
        /// <param name="tokenizer"></param>
        public BufferedAbcTokenizer(AbcTokenizer tokenizer)
            => Tokenizer = tokenizer;

        /// <summary>
        ///     base tokenizer
        /// </summary>
        public AbcTokenizer Tokenizer { get; }

        private void FetchToken() {
            if (Tokenizer.HasToken)
                Tokenizer.ReadNextToken();
            tokens.Enqueue(Tokenizer.CurrentToken);
        }

        /// <summary>
        ///     look a head a few symbols
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AbcCharacterReference Lookahead(int index) {
            while (tokens.Count <= index + 1)
                FetchToken();

            if (index >= tokens.Count)
                return Tokenizer.CharCache.FromCache(string.Empty, string.Empty, Model.TokenKind.Eof);

            var i = 0;
            foreach (var item in tokens) {
                if (i == index)
                    return item;
                i++;
            }

            return Tokenizer.CharCache.FromCache(string.Empty, string.Empty, Model.TokenKind.Eof);
        }

        internal void NextToken()
            => tokens.Dequeue();

        /// <summary>
        ///     dispose
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
        ///     dispose this tokenizer
        /// </summary>
        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
