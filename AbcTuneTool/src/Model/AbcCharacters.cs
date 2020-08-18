namespace AbcTuneTool.Model {

    /// <summary>
    ///     ABC character helpers
    /// </summary>
    public static class AbcCharacters {

        /// <summary>
        ///     undefined character
        /// </summary>
        public static readonly AbcCharacter Undefined
            = new AbcCharacter(AbcCharacterKind.Undefined, '\0', string.Empty);

        /// <summary>
        ///     EOF character
        /// </summary>
        internal static readonly AbcCharacter Eof
            = new AbcCharacter(AbcCharacterKind.Eof, '\0', string.Empty);
    }
}
