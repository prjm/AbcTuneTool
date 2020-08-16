namespace AbcTuneTool.Model
{

    /// <summary>
    ///     ABC character helpers
    /// </summary>
    public static class AbcCharacters
    {

        /// <summary>
        ///     undefined character
        /// </summary>
        public static readonly AbcCharacter Undefined
            = new AbcCharacter(AbcCharacterKind.Undefined, '\x0000');

        /// <summary>
        ///     eof character
        /// </summary>
        internal static readonly AbcCharacter Eof
            = new AbcCharacter(AbcCharacterKind.Eof, '\x0000');
    }
}
