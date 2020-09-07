namespace AbcTuneTool.Model {

    /// <summary>
    ///     mixolydian key table
    /// </summary>
    public class MixolydianKeyTable : KeyTable {

        /// <summary>
        ///     create a mixolydian key table
        /// </summary>
        public MixolydianKeyTable() : base(new StandardToneSystem(KnownStrings.MixolydianMode, 7)) {

            AddKey(+7, 'G', '#');
            AddKey(+6, 'C', '#');
            AddKey(+5, 'F', '#');
            AddKey(+4, 'B', ' ');
            AddKey(+3, 'E', ' ');
            AddKey(+2, 'A', ' ');
            AddKey(+1, 'D', ' ');
            AddKey(+0, 'G', ' ');
            AddKey(-1, 'C', ' ');
            AddKey(-2, 'F', ' ');
            AddKey(-3, 'B', 'b');
            AddKey(-4, 'E', 'b');
            AddKey(-5, 'A', 'b');
            AddKey(-6, 'D', 'b');
            AddKey(-7, 'G', 'b');

        }
    }
}
