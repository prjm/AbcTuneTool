namespace AbcTuneTool.Model {

    /// <summary>
    ///     ABC character kind
    /// </summary>
    public enum TokenKind {

        /// <summary>
        ///     undefined kind of token
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     standard character
        /// </summary>
        Char = 1,

        /// <summary>
        ///     character encoded by mnemonic
        /// </summary>
        Mnenomic = 2,

        /// <summary>
        ///     character encoded by entity
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
        ///     end of file
        /// </summary>
        Eof = 6,

        /// <summary>
        ///     backlash sign
        /// </summary>
        Backslash = 7,

        /// <summary>
        ///     percent sign
        /// </summary>
        Percent = 8,

        /// <summary>
        ///     ampersand sign
        /// </summary>
        Ampersand = 9,

        /// <summary>
        ///     dollar sign
        /// </summary>
        Dollar = 10,

        /// <summary>
        ///     font size
        /// </summary>
        FontSize = 11,

        /// <summary>
        ///     line of comment / end of line comment
        /// </summary>
        Comment = 12,

        /// <summary>
        ///     line continuation
        /// </summary>
        LineContinuation = 13,

        /// <summary>
        ///     empty line
        /// </summary>
        EmptyLine = 14,

        /// <summary>
        ///     line break
        /// </summary>
        Linebreak = 15,

        /// <summary>
        ///     information field header
        /// </summary>
        InformationFieldHeader = 16,

        /// <summary>
        ///     header continuation
        /// </summary>
        HeaderContinuation = 17,
    }
}
