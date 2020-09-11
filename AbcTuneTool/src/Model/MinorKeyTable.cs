namespace AbcTuneTool.Model {

    /// <summary>
    ///     minor key table
    /// </summary>
    public class MinorKeyTable : KeyTable {

        /// <summary>
        ///     create a new minor key table
        /// </summary>
        public MinorKeyTable() : base(new StandardToneSystem(KnownStrings.MinorMode, 9)) {
            AddKey(-1, 'D', ' ', new Tone('b', 'b'));
            AddKey(-2, 'G', ' ', new Tone('e', 'b'));
            AddKey(-3, 'C', ' ', new Tone('a', 'b'));
            AddKey(-4, 'F', ' ', new Tone('d', 'b'));
            AddKey(-5, 'B', 'b', new Tone('g', 'b'));
            AddKey(-6, 'E', 'b', new Tone('c', 'b'));
            AddKey(-7, 'A', 'b', new Tone('f', 'b'));
            AddKey(+0, 'A', ' ');
            AddKey(+1, 'E', ' ');
            AddKey(+2, 'B', ' ');
            AddKey(+3, 'F', '#');
            AddKey(+4, 'C', '#');
            AddKey(+5, 'G', '#');
            AddKey(+6, 'D', '#', new Tone('e', '#'));
            AddKey(+7, 'A', '#', new Tone('b', '#'));
        }
    }
}
