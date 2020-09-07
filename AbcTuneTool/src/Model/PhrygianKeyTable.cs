namespace AbcTuneTool.Model {

    /// <summary>
    ///     phrygian key table
    /// </summary>
    public class PhrygianKeyTable : KeyTable {

        /// <summary>
        ///     create a new phrygian key table
        /// </summary>
        public PhrygianKeyTable() : base(new StandardToneSystem(KnownStrings.PhrygianMode, 4)) {

            AddKey(+7, 'E', '#');
            AddKey(+6, 'A', '#');
            AddKey(+5, 'D', '#');
            AddKey(+4, 'G', '#');
            AddKey(+3, 'C', '#');
            AddKey(+2, 'F', '#');
            AddKey(+1, 'B', ' ');
            AddKey(+0, 'E', ' ');
            AddKey(-1, 'A', ' ');
            AddKey(-2, 'D', ' ');
            AddKey(-3, 'G', ' ');
            AddKey(-4, 'C', ' ');
            AddKey(-5, 'F', ' ');
            AddKey(-6, 'B', 'b');
            AddKey(-7, 'E', 'b');

        }

    }
}
