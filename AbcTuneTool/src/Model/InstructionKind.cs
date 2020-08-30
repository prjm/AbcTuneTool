namespace AbcTuneTool.Model {

    /// <summary>
    ///     instruction kind
    /// </summary>
    public enum InstructionKind {

        /// <summary>
        ///     undefined instruction
        /// </summary>
        Undefied = 0,

        /// <summary>
        ///     charset instruction
        /// </summary>
        Charset = 1,

        /// <summary>
        ///     version instruction
        /// </summary>
        Version = 2,

        /// <summary>
        ///     include instruction
        /// </summary>
        Include = 3,

        /// <summary>
        ///     creator instruction
        /// </summary>
        Creator = 4,

        /// <summary>
        ///     decoration instruction
        /// </summary>
        Decoration = 5,

        /// <summary>
        ///     linebreak instruction
        /// </summary>
        Linebreak = 6,

        /// <summary>
        ///     other instruction
        /// </summary>
        Otherwise = 7,
    }
}
