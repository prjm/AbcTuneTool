namespace AbcTuneTool.Model {

    /// <summary>
    ///     lydian key table
    /// </summary>
    public class LydianKeyTable : KeyTable {

        /// <summary>
        ///     create a new lydian key table
        /// </summary>
        public LydianKeyTable() : base(new StandardToneSystem(KnownStrings.LydianMode, 5)) {

            AddKey(+7, 'F', '#');
            AddKey(+6, 'B', ' ');
            AddKey(+5, 'E', ' ');
            AddKey(+4, 'A', ' ');
            AddKey(+3, 'D', ' ');
            AddKey(+2, 'G', ' ');
            AddKey(+1, 'C', ' ');
            AddKey(+0, 'F', ' ');
            AddKey(-1, 'B', 'b');
            AddKey(-2, 'E', 'b');
            AddKey(-3, 'A', 'b');
            AddKey(-4, 'D', 'b');
            AddKey(-5, 'G', 'b');
            AddKey(-6, 'C', 'b');
            AddKey(-7, 'F', 'b');


        }

    }
}
