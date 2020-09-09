namespace AbcTuneTool.Model {

    /// <summary>
    ///     major key table
    /// </summary>
    public class MajorKeyTable : KeyTable {

        /// <summary>
        ///     create the major key table
        /// </summary>
        public MajorKeyTable() : base(new StandardToneSystem(KnownStrings.MajorMode)) {

            AddKey(-1, 'F', ' ');
            AddKey(-2, 'B', 'b', new Tone('B', 'b'));
            AddKey(-3, 'E', 'b', new Tone('E', 'b'));
            AddKey(-4, 'A', 'b', new Tone('A', 'b'));
            AddKey(-5, 'D', 'b', new Tone('D', 'b'));
            AddKey(-6, 'G', 'b', new Tone('G', 'b'));
            AddKey(-7, 'C', 'b', new Tone('C', 'b'));

            AddKey(+0, 'C', ' ');
            AddKey(+1, 'G', ' ');
            AddKey(+2, 'D', ' ');
            AddKey(+3, 'A', ' ');
            AddKey(+4, 'E', ' ');
            AddKey(+5, 'B', ' ');
            AddKey(+6, 'F', '#', new Tone('E', '#'), new Tone('F', '#'));
            AddKey(+7, 'C', '#', new Tone('B', '#'));
        }
    }
}
