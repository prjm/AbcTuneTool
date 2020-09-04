using System;

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
        ///     minor scale
        /// </summary>
        public KeyNotesMode Minor { get; }
            = new KeyNotesMode(KnownStrings.Minor);

        /// <summary>
        ///     mixolydian scale
        /// </summary>
        public KeyNotesMode Mixolydian { get; }
            = new KeyNotesMode(KnownStrings.Mixolydian);

        /// <summary>
        ///     dorian scale
        /// </summary>
        public KeyNotesMode Dorian { get; }
            = new KeyNotesMode(KnownStrings.Dorian);

        /// <summary>
        ///     phrygian scale
        /// </summary>
        public KeyNotesMode Phrygian { get; }
            = new KeyNotesMode(KnownStrings.Phrygian);

        /// <summary>
        ///     lydian scale
        /// </summary>
        public KeyNotesMode Lydian { get; }
            = new KeyNotesMode(KnownStrings.Lydian);

        /// <summary>
        ///     locrian scale
        /// </summary>
        public KeyNotesMode Locrian { get; }
            = new KeyNotesMode(KnownStrings.Locrian);

        /// <summary>
        ///     possible key modes
        /// </summary>
        public KeyNotes() {
            InitMajorScale();
            InitMinorScale();
            InitMixolydianScale();
            InitDorianScale();
            InitPhrygianScale();
            InitLydianScale();
            InitLocrianScale();
        }

        private void InitLocrianScale() {
            Locrian.AddNote('B', Accidental.Sharp);
            Locrian.AddNote('E', Accidental.Sharp);
            Locrian.AddNote('A', Accidental.Sharp);
            Locrian.AddNote('D', Accidental.Sharp);
            Locrian.AddNote('G', Accidental.Sharp);
            Locrian.AddNote('C', Accidental.Sharp);
            Locrian.AddNote('F', Accidental.Sharp);
            Locrian.AddNote('B', Accidental.Undefined);
            Locrian.AddNote('E', Accidental.Undefined);
            Locrian.AddNote('A', Accidental.Undefined);
            Locrian.AddNote('D', Accidental.Undefined);
            Locrian.AddNote('G', Accidental.Undefined);
            Locrian.AddNote('C', Accidental.Undefined);
            Locrian.AddNote('F', Accidental.Undefined);
            Locrian.AddNote('B', Accidental.Flat);
        }

        private void InitLydianScale() {
            Lydian.AddNote('F', Accidental.Sharp);
            Lydian.AddNote('B', Accidental.Undefined);
            Lydian.AddNote('E', Accidental.Undefined);
            Lydian.AddNote('A', Accidental.Undefined);
            Lydian.AddNote('D', Accidental.Undefined);
            Lydian.AddNote('G', Accidental.Undefined);
            Lydian.AddNote('C', Accidental.Undefined);
            Lydian.AddNote('F', Accidental.Undefined);
            Lydian.AddNote('B', Accidental.Flat);
            Lydian.AddNote('E', Accidental.Flat);
            Lydian.AddNote('A', Accidental.Flat);
            Lydian.AddNote('D', Accidental.Flat);
            Lydian.AddNote('G', Accidental.Flat);
            Lydian.AddNote('C', Accidental.Flat);
            Lydian.AddNote('F', Accidental.Flat);
        }

        private void InitPhrygianScale() {
            Phrygian.AddNote('E', Accidental.Sharp);
            Phrygian.AddNote('A', Accidental.Sharp);
            Phrygian.AddNote('D', Accidental.Sharp);
            Phrygian.AddNote('G', Accidental.Sharp);
            Phrygian.AddNote('C', Accidental.Sharp);
            Phrygian.AddNote('F', Accidental.Sharp);
            Phrygian.AddNote('B', Accidental.Undefined);
            Phrygian.AddNote('E', Accidental.Undefined);
            Phrygian.AddNote('A', Accidental.Undefined);
            Phrygian.AddNote('D', Accidental.Undefined);
            Phrygian.AddNote('G', Accidental.Undefined);
            Phrygian.AddNote('C', Accidental.Undefined);
            Phrygian.AddNote('F', Accidental.Undefined);
            Phrygian.AddNote('B', Accidental.Flat);
            Phrygian.AddNote('E', Accidental.Flat);
        }

        private void InitDorianScale() {
            Dorian.AddNote('D', Accidental.Sharp);
            Dorian.AddNote('G', Accidental.Sharp);
            Dorian.AddNote('C', Accidental.Sharp);
            Dorian.AddNote('F', Accidental.Sharp);
            Dorian.AddNote('B', Accidental.Undefined);
            Dorian.AddNote('E', Accidental.Undefined);
            Dorian.AddNote('A', Accidental.Undefined);
            Dorian.AddNote('D', Accidental.Undefined);
            Dorian.AddNote('G', Accidental.Undefined);
            Dorian.AddNote('C', Accidental.Undefined);
            Dorian.AddNote('F', Accidental.Undefined);
            Dorian.AddNote('B', Accidental.Flat);
            Dorian.AddNote('E', Accidental.Flat);
            Dorian.AddNote('A', Accidental.Flat);
            Dorian.AddNote('D', Accidental.Flat);
        }

        private void InitMixolydianScale() {
            Mixolydian.AddNote('G', Accidental.Sharp);
            Mixolydian.AddNote('C', Accidental.Sharp);
            Mixolydian.AddNote('F', Accidental.Sharp);
            Mixolydian.AddNote('B', Accidental.Undefined);
            Mixolydian.AddNote('E', Accidental.Undefined);
            Mixolydian.AddNote('A', Accidental.Undefined);
            Mixolydian.AddNote('D', Accidental.Undefined);
            Mixolydian.AddNote('G', Accidental.Undefined);
            Mixolydian.AddNote('C', Accidental.Undefined);
            Mixolydian.AddNote('F', Accidental.Undefined);
            Mixolydian.AddNote('B', Accidental.Flat);
            Mixolydian.AddNote('E', Accidental.Flat);
            Mixolydian.AddNote('A', Accidental.Flat);
            Mixolydian.AddNote('D', Accidental.Flat);
            Mixolydian.AddNote('G', Accidental.Flat);
        }

        private void InitMinorScale() {
            Minor.AddNote('A', Accidental.Sharp);
            Minor.AddNote('D', Accidental.Sharp);
            Minor.AddNote('G', Accidental.Sharp);
            Minor.AddNote('C', Accidental.Sharp);
            Minor.AddNote('F', Accidental.Sharp);
            Minor.AddNote('B', Accidental.Undefined);
            Minor.AddNote('E', Accidental.Undefined);
            Minor.AddNote('A', Accidental.Undefined);
            Minor.AddNote('D', Accidental.Undefined);
            Minor.AddNote('G', Accidental.Undefined);
            Minor.AddNote('C', Accidental.Undefined);
            Minor.AddNote('F', Accidental.Undefined);
            Minor.AddNote('B', Accidental.Flat);
            Minor.AddNote('E', Accidental.Flat);
            Minor.AddNote('A', Accidental.Flat);
        }

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

            if (string.IsNullOrWhiteSpace(mode) || mode.StartsWith(KnownStrings.Maj, StringComparison.OrdinalIgnoreCase)) {
                return Major.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Min, StringComparison.OrdinalIgnoreCase) || string.Equals(mode, KnownStrings.M, StringComparison.Ordinal)) {
                return Minor.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Mix, StringComparison.OrdinalIgnoreCase)) {
                return Mixolydian.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Dor, StringComparison.OrdinalIgnoreCase)) {
                return Dorian.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Phr, StringComparison.OrdinalIgnoreCase)) {
                return Phrygian.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Lyd, StringComparison.OrdinalIgnoreCase)) {
                return Lydian.GetNote(note, accidental);
            }

            if (mode.StartsWith(KnownStrings.Loc, StringComparison.OrdinalIgnoreCase)) {
                return Locrian.GetNote(note, accidental);
            }


            return default!;
        }
    }
}
