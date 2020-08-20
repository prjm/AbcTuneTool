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
        public AbcCharacterReference(string value, string originalValue, AbcCharacterKind kind)
            => AbcChar = new AbcCharacter(value, originalValue, kind);

        private AbcCharacter token;

        /// <summary>
        ///     current token
        /// </summary>
        public ref AbcCharacter AbcChar
            => ref token;

    }
}
