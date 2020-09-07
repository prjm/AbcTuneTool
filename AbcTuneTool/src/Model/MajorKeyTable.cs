namespace AbcTuneTool.Model {

    /// <summary>
    ///     major key table
    /// </summary>
    public class MajorKeyTable : KeyTable {

        /// <summary>
        ///     create the major key table
        /// </summary>
        public MajorKeyTable() : base(new StandardToneSystem(KnownStrings.MajorMode)) {
            AddKey(-7, 'C', 'b');
            AddKey(-6, 'G', 'b');
            AddKey(-5, 'D', 'b');
            AddKey(-4, 'A', 'b');
            AddKey(-3, 'E', 'b');
            AddKey(-2, 'B', 'b');
            AddKey(-1, 'F', ' ');
            AddKey(+0, 'C', ' ');
            AddKey(+1, 'G', ' ');
            AddKey(+2, 'D', ' ');
            AddKey(+3, 'A', ' ');
            AddKey(+4, 'E', ' ');
            AddKey(+5, 'B', ' ');
            AddKey(+6, 'F', '#');
            AddKey(+7, 'C', '#');
        }
    }
}
