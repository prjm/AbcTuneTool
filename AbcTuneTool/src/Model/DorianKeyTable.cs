namespace AbcTuneTool.Model {

    /// <summary>
    ///     dorian key table
    /// </summary>
    public class DorianKeyTable : KeyTable {

        /// <summary>
        ///     dorian key modes
        /// </summary>
        public DorianKeyTable() : base(new StandardToneSystem(KnownStrings.DorianMode, 2)) {

            AddKey(+7, 'D', '#');
            AddKey(+6, 'G', '#');
            AddKey(+5, 'C', '#');
            AddKey(+4, 'F', '#');
            AddKey(+3, 'B', ' ');
            AddKey(+2, 'E', ' ');
            AddKey(+1, 'A', ' ');
            AddKey(+0, 'D', ' ');
            AddKey(-1, 'G', ' ');
            AddKey(-2, 'C', ' ');
            AddKey(-3, 'F', ' ');
            AddKey(-4, 'B', 'b');
            AddKey(-5, 'E', 'b');
            AddKey(-6, 'A', 'b');
            AddKey(-7, 'D', 'b');

        }

    }
}
