namespace AbcTuneTool.Model.KeyTables {

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
            AddKey(+6, 'B', ' ', new Tone('e', '#'));
            AddKey(+7, 'F', '#', new Tone('b', '#'));
            AddKey(+0, 'F', ' ');
            AddKey(-1, 'B', 'b', new Tone('b', 'b'));
            AddKey(-2, 'E', 'b', new Tone('e', 'b'));
            AddKey(-3, 'A', 'b', new Tone('a', 'b'));
            AddKey(-4, 'D', 'b', new Tone('d', 'b'));
            AddKey(-5, 'G', 'b', new Tone('g', 'b'));
            AddKey(-6, 'C', 'b', new Tone('c', 'b'));
            AddKey(-7, 'F', 'b', new Tone('f', 'b'));


        }

    }
}
