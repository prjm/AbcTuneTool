using AbcTuneTool.Model;
using System;
using System.IO;

namespace AbcTuneTool.FileIo
{

    /// <summary>
    ///     tokenizer for ABC files
    /// </summary>
    public class AbcTokenizer : IDisposable
    {

        private AbcCharacter currentToken;
        private bool disposedValue;

        /// <summary>
        ///     create a new tokenizer
        /// </summary>
        /// <param name="reader"></param>
        public AbcTokenizer(TextReader reader)
        {
            currentToken = AbcCharacters.Undefined;
            Reader = reader;
        }

        /// <summary>
        ///     stream reader
        /// </summary>
        public TextReader Reader { get; }

        /// <summary>
        ///     check if there are token left
        /// </summary>
        public bool HasToken
            => currentToken.Kind != AbcCharacterKind.Eof;

        public AbcCharacter CurrentToken
            => currentToken;

        /// <summary>
        ///     read the next toke
        /// </summary>
        public void ReadNextToken()
        {
            if (!HasToken)
                return;

            var c = Reader.Read();
            if (c < 0)
            {
                MarkEof();
                return;
            }

            var value = (char)c;


            if (value == '\\')
            {
                ReadMnenomic();
                return;
            }



        }

        private void MarkEof()
            => currentToken = AbcCharacters.Eof;

        private void ReadMnenomic()
        {
            var c = Reader.Read();
            if (c < 0)
            {
                MarkEof();
                return;
            }

            var decorator = (char)c;

            c = Reader.Read();
            if (c < 0)
            {
                MarkEof();
                return;
            }

            var decoratedElement = (char)c;
            currentToken = new AbcCharacter(AbcCharacterKind.Mnenomic, Mnemonics.Decode(decorator, decoratedElement));
        }

        /// <summary>
        ///     dispose this tokenizer
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Reader.Dispose();
                }
                disposedValue = true;
            }
        }

        /// <summary>
        ///     dispose this tokenizer
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
