namespace AbcTuneTool.Model {

    /// <summary>
    ///     notes for keys
    /// </summary>
    public class KeyNotes {


        /// <summary>
        ///     major scale
        /// </summary>
        public KeyNotesMode Major { get; }
            = new KeyNotesMode(KnownStrings.Major);

        /// <summary>
        ///     possible key modes
        /// </summary>
        public KeyNotes()
            => InitMajorScale();

        private void InitMajorScale() {
            Major.AddNote('C', Accidental.Flat);
            Major.AddNote('G', Accidental.Flat);
            Major.AddNote('D', Accidental.Flat);
            Major.AddNote('A', Accidental.Flat);
            Major.AddNote('E', Accidental.Flat);
            Major.AddNote('B', Accidental.Flat);
            Major.AddNote('F', Accidental.Undefined);
            Major.AddNote('C', Accidental.Undefined);
            Major.AddNote('G', Accidental.Undefined);
            Major.AddNote('D', Accidental.Undefined);
            Major.AddNote('A', Accidental.Undefined);
            Major.AddNote('E', Accidental.Undefined);
            Major.AddNote('B', Accidental.Undefined);
            Major.AddNote('F', Accidental.Sharp);
            Major.AddNote('C', Accidental.Sharp);
        }

        /// <summary>
        ///     get the key signature
        /// </summary>
        /// <param name="note"></param>
        /// <param name="accidental"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Note ForKeySignature(char note, Accidental accidental, string mode) {

            if (string.IsNullOrWhiteSpace(mode)) {
                return Major.GetNote(note, accidental);
            }

            return default!;
        }
    }
}
