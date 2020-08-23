using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     char reference
    /// </summary>
    public class AbcCharacterReference {


        /// <summary>
        ///     create a new char reference
        /// </summary>
        /// <param name="value"></param>
        /// <param name="originalValue"></param>
        /// <param name="kind"></param>
        public AbcCharacterReference(string value, string originalValue, TokenKind kind)
            => AbcChar = new Token(value, originalValue, kind);

        private Token token;

        /// <summary>
        ///     current token
        /// </summary>
        public ref Token AbcChar
            => ref token;

    }
}
