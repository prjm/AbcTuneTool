namespace AbcTuneTool.Model.KeyTables {

    /// <summary>
    ///     phrygian key table
    /// </summary>
    public class PhrygianKeyTable : KeyTable {

        /// <summary>
        ///     create a new phrygian key table
        /// </summary>
        public PhrygianKeyTable() : base(new StandardToneSystem(KnownStrings.PhrygianMode, 4)) {

            AddKey(+1, 'B', ' ');
            AddKey(+2, 'F', '#');
            AddKey(+3, 'C', '#');
            AddKey(+4, 'G', '#');
            AddKey(+5, 'D', '#');
            AddKey(+6, 'A', '#', new Tone('e', '#'));
            AddKey(+7, 'E', '#', new Tone('b', '#'));

            AddKey(+0, 'E', ' ');
            AddKey(-1, 'A', ' ', new Tone('b', 'b'));
            AddKey(-2, 'D', ' ', new Tone('e', 'b'));
            AddKey(-3, 'G', ' ', new Tone('a', 'b'));
            AddKey(-4, 'C', ' ', new Tone('d', 'b'));
            AddKey(-5, 'F', ' ', new Tone('g', 'b'));
            AddKey(-6, 'B', 'b', new Tone('c', 'b'));
            AddKey(-7, 'E', 'b', new Tone('f', 'b'));

        }

    }
}
