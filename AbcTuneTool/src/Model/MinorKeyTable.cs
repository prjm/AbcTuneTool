namespace AbcTuneTool.Model {

    /// <summary>
    ///     minor key table
    /// </summary>
    public class MinorKeyTable : KeyTable {

        /// <summary>
        ///     create a new minor key table
        /// </summary>
        public MinorKeyTable() : base(new StandardToneSystem(KnownStrings.MinorMode, 9)) {
            AddKey(-7, 'A', 'b');
            AddKey(-6, 'E', 'b');
            AddKey(-5, 'B', 'b');
            AddKey(-4, 'F', ' ');
            AddKey(-3, 'C', ' ');
            AddKey(-2, 'G', ' ');
            AddKey(-1, 'D', ' ');
            AddKey(+0, 'A', ' ');
            AddKey(+1, 'E', ' ');
            AddKey(+2, 'B', ' ');
            AddKey(+3, 'F', '#');
            AddKey(+4, 'C', '#');
            AddKey(+5, 'G', '#');
            AddKey(+6, 'D', '#');
            AddKey(+7, 'A', '#');
        }
    }
}
