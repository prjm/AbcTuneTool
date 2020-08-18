namespace AbcTuneTool.Model {

    /// <summary>
    ///     ABC character helpers
    /// </summary>
    public static class AbcCharacters {

        /// <summary>
        ///     undefined character
        /// </summary>
        public static readonly AbcCharacter Undefined
            = new AbcCharacter(string.Empty, string.Empty, AbcCharacterKind.Undefined);

        /// <summary>
        ///     EOF character
        /// </summary>
        internal static readonly AbcCharacter Eof
            = new AbcCharacter(string.Empty, string.Empty, AbcCharacterKind.Eof);
    }
}
