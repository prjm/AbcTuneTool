namespace AbcTuneTool.Model.KeyTables {

    /// <summary>
    ///     locrian key table
    /// </summary>
    public class LocrianKeyTable : KeyTable {

        /// <summary>
        ///     create a new locrian key table
        /// </summary>
        public LocrianKeyTable() : base(new StandardToneSystem(KnownStrings.LocrianMode, 11)) {

            AddKey(+1, 'F', '#');
            AddKey(+2, 'C', '#');
            AddKey(+3, 'G', '#');
            AddKey(+4, 'D', '#');
            AddKey(+5, 'A', '#');
            AddKey(+6, 'E', '#', new Tone('e', '#'));
            AddKey(+7, 'B', '#', new Tone('b', '#'));
            AddKey(+0, 'B', ' ');

            AddKey(-1, 'E', ' ', new Tone('b', 'b'));
            AddKey(-2, 'A', ' ', new Tone('e', 'b'));
            AddKey(-3, 'D', ' ', new Tone('a', 'b'));
            AddKey(-4, 'G', ' ', new Tone('d', 'b'));
            AddKey(-5, 'C', ' ', new Tone('g', 'b'));
            AddKey(-6, 'F', ' ', new Tone('c', 'b'));
            AddKey(-7, 'B', 'b', new Tone('f', 'b'));

        }

    }
}
