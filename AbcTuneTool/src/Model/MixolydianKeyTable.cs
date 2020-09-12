namespace AbcTuneTool.Model {

    /// <summary>
    ///     mixolydian key table
    /// </summary>
    public class MixolydianKeyTable : KeyTable {

        /// <summary>
        ///     create a mixolydian key table
        /// </summary>
        public MixolydianKeyTable() : base(new StandardToneSystem(KnownStrings.MixolydianMode, 7)) {

            AddKey(+1, 'D', ' ');
            AddKey(+2, 'A', ' ');
            AddKey(+3, 'E', ' ');
            AddKey(+4, 'B', ' ');
            AddKey(+5, 'F', '#');
            AddKey(+6, 'C', '#', new Tone('e', '#'));
            AddKey(+7, 'G', '#', new Tone('b', '#'));
            AddKey(+0, 'G', ' ');
            AddKey(-1, 'C', ' ', new Tone('b', 'b'));
            AddKey(-2, 'F', ' ', new Tone('e', 'b'));
            AddKey(-3, 'B', 'b', new Tone('a', 'b'));
            AddKey(-4, 'E', 'b', new Tone('d', 'b'));
            AddKey(-5, 'A', 'b', new Tone('g', 'b'));
            AddKey(-6, 'D', 'b', new Tone('c', 'b'));
            AddKey(-7, 'G', 'b', new Tone('f', 'b'));

        }
    }
}
