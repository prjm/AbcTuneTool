namespace AbcTuneTool.Model {

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
            AddKey(+6, 'A', '#');
            AddKey(+7, 'E', '#', new Tone('E', '#'));
            AddKey(+0, 'E', ' ');
            AddKey(-1, 'A', ' ', new Tone('B', 'b'));
            AddKey(-2, 'D', ' ', new Tone('E', 'b'));
            AddKey(-3, 'G', ' ');
            AddKey(-4, 'C', ' ');
            AddKey(-5, 'F', ' ');
            AddKey(-6, 'B', 'b');
            AddKey(-7, 'E', 'b', new Tone('E', 'b'));

        }

    }
}
