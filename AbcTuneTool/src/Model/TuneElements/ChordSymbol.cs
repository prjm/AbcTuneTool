using AbcTuneTool.Model.Symbolic;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     chord symbol
    /// </summary>
    public class ChordSymbol : TuneElement {

        /// <summary>
        ///     create a new chord symbol
        /// </summary>
        /// <param name="firstNote"></param>
        /// <param name="accidental"></param>
        /// <param name="type"></param>
        /// <param name="bassNote"></param>
        /// <param name="bassAccidental"></param>
        public ChordSymbol(char firstNote, Accidental accidental, string type, char bassNote, Accidental bassAccidental) {
            Note = firstNote;
            Accidental = accidental;
            Type = type;
            BassNote = bassNote;
            BassAccidental = bassAccidental;
        }

        /// <summary>
        ///     note
        /// </summary>
        public char Note { get; }

        /// <summary>
        ///     accidental
        /// </summary>
        public Accidental Accidental { get; }

        /// <summary>
        ///     chord type
        /// </summary>
        public string Type { get; }

        /// <summary>
        ///     bass note
        /// </summary>
        public char BassNote { get; }

        /// <summary>
        ///     bass accidental
        /// </summary>
        public Accidental BassAccidental { get; }
    }
}
