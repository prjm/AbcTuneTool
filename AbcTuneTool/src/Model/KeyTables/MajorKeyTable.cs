namespace AbcTuneTool.Model.KeyTables {

    /// <summary>
    ///     major key table
    /// </summary>
    public class MajorKeyTable : KeyTable {

        /// <summary>
        ///     create the major key table
        /// </summary>
        public MajorKeyTable() : base(new StandardToneSystem(KnownStrings.MajorMode)) {

            AddKey(-1, 'F', ' ', new Tone('b', 'b'));
            AddKey(-2, 'B', 'b', new Tone('e', 'b'));
            AddKey(-3, 'E', 'b', new Tone('a', 'b'));
            AddKey(-4, 'A', 'b', new Tone('d', 'b'));
            AddKey(-5, 'D', 'b', new Tone('g', 'b'));
            AddKey(-6, 'G', 'b', new Tone('c', 'b'));
            AddKey(-7, 'C', 'b', new Tone('f', 'b'));

            AddKey(+0, 'C', ' ');
            AddKey(+1, 'G', ' ');
            AddKey(+2, 'D', ' ');
            AddKey(+3, 'A', ' ');
            AddKey(+4, 'E', ' ');
            AddKey(+5, 'B', ' ');
            AddKey(+6, 'F', '#', new Tone('e', '#'), new Tone('f', '#'));
            AddKey(+7, 'C', '#', new Tone('b', '#'));
        }
    }
}
