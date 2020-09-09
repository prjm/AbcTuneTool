namespace AbcTuneTool.Model {

    /// <summary>
    ///     minor key table
    /// </summary>
    public class MinorKeyTable : KeyTable {

        /// <summary>
        ///     create a new minor key table
        /// </summary>
        public MinorKeyTable() : base(new StandardToneSystem(KnownStrings.MinorMode, 9)) {
            AddKey(-1, 'D', ' ');
            AddKey(-2, 'G', ' ');
            AddKey(-3, 'C', ' ');
            AddKey(-4, 'F', ' ');
            AddKey(-5, 'B', 'b', new Tone('B', 'b'));
            AddKey(-6, 'E', 'b', new Tone('E', 'b'));
            AddKey(-7, 'A', 'b', new Tone('A', 'b'));
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
