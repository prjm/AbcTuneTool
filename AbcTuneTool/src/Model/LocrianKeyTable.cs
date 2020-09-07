namespace AbcTuneTool.Model {

    /// <summary>
    ///     locrian key table
    /// </summary>
    public class LocrianKeyTable : KeyTable {

        /// <summary>
        ///     create a new locrian key table
        /// </summary>
        public LocrianKeyTable() : base(new StandardToneSystem(KnownStrings.LocrianMode, 11)) {

            AddKey(+7, 'B', '#');
            AddKey(+6, 'E', '#');
            AddKey(+5, 'A', '#');
            AddKey(+4, 'D', '#');
            AddKey(+3, 'G', '#');
            AddKey(+2, 'C', '#');
            AddKey(+1, 'F', '#');
            AddKey(+0, 'B', ' ');
            AddKey(-1, 'E', ' ');
            AddKey(-2, 'A', ' ');
            AddKey(-3, 'D', ' ');
            AddKey(-4, 'G', ' ');
            AddKey(-5, 'C', ' ');
            AddKey(-6, 'F', ' ');
            AddKey(-7, 'B', 'b');

        }

    }
}
