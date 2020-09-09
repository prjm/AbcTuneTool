namespace AbcTuneTool.Model {

    /// <summary>
    ///     lydian key table
    /// </summary>
    public class LydianKeyTable : KeyTable {

        /// <summary>
        ///     create a new lydian key table
        /// </summary>
        public LydianKeyTable() : base(new StandardToneSystem(KnownStrings.LydianMode, 5)) {

            AddKey(+1, 'C', ' ');
            AddKey(+2, 'G', ' ');
            AddKey(+3, 'D', ' ');
            AddKey(+4, 'A', ' ');
            AddKey(+5, 'E', ' ');
            AddKey(+6, 'B', ' ');
            AddKey(+7, 'F', '#');
            AddKey(+0, 'F', ' ');
            AddKey(-1, 'B', 'b', new Tone('B', 'b'));
            AddKey(-2, 'E', 'b', new Tone('E', 'b'));
            AddKey(-3, 'A', 'b', new Tone('A', 'b'));
            AddKey(-4, 'D', 'b', new Tone('D', 'b'));
            AddKey(-5, 'G', 'b', new Tone('G', 'b'));
            AddKey(-6, 'C', 'b', new Tone('C', 'b'));
            AddKey(-7, 'F', 'b', new Tone('F', 'b'));


        }

    }
}
