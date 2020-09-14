namespace AbcTuneTool.Model {

    /// <summary>
    ///     dorian key table
    /// </summary>
    public class DorianKeyTable : KeyTable {

        /// <summary>
        ///     dorian key modes
        /// </summary>
        public DorianKeyTable() : base(new StandardToneSystem(KnownStrings.DorianMode, 2)) {

            AddKey(+1, 'A', ' ');
            AddKey(+2, 'E', ' ');
            AddKey(+3, 'B', ' ');
            AddKey(+4, 'F', '#');
            AddKey(+5, 'C', '#');
            AddKey(+6, 'G', '#', new Tone('e', '#'));
            AddKey(+7, 'D', '#', new Tone('b', '#'));

            AddKey(+0, 'D', ' ');
            AddKey(-1, 'G', ' ', new Tone('b', 'b'));
            AddKey(-2, 'C', ' ', new Tone('e', 'b'));
            AddKey(-3, 'F', ' ', new Tone('a', 'b'));
            AddKey(-4, 'B', 'b', new Tone('d', 'b'));
            AddKey(-5, 'E', 'b', new Tone('g', 'b'));
            AddKey(-6, 'A', 'b', new Tone('c', 'b'));
            AddKey(-7, 'D', 'b', new Tone('f', 'b'));

        }

    }
}
