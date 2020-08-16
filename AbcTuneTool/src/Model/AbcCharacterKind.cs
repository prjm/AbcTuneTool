namespace AbcTuneTool.Model
{

    /// <summary>
    ///     ABC character kind
    /// </summary>
    public enum AbcCharacterKind
    {

        /// <summary>
        ///     undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     standard character
        /// </summary>
        Char = 1,

        /// <summary>
        ///     mnemonic
        /// </summary>
        Mnenomic = 2,

        /// <summary>
        ///     entity
        /// </summary>
        Entity = 3,

        /// <summary>
        ///     fixed UNICODE 2 byte
        /// </summary>
        FixedUnicody2Byte = 4,

        /// <summary>
        ///     fixed UNICODE 4 byte
        /// </summary>
        FixedUnicode4Byte = 5,

        /// <summary>
        ///     end of fle
        /// </summary>
        Eof = 6,

    }
}
