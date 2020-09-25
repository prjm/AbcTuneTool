namespace AbcTuneTool.Model.Symbolic {

    /// <summary>
    ///     clef settings
    /// </summary>
    public class ClefSettings {

        /// <summary>
        ///     create new clef settings
        /// </summary>
        /// <param name="clef"></param>
        /// <param name="clefLine"></param>
        /// <param name="clefTranspose"></param>
        /// <param name="middle"></param>
        /// <param name="transpose"></param>
        /// <param name="octaves"></param>
        /// <param name="stafflines"></param>
        public ClefSettings(ClefMode clef, int clefLine, ClefTranspose clefTranspose, char middle, int transpose, int octaves, int stafflines) {
            Clef = clef;
            ClefLine = clefLine;
            ClefTranspose = clefTranspose;
            Middle = middle;
            Transpose = transpose;
            Octaves = octaves;
            Stafflines = stafflines;
        }

        /// <summary>
        ///     clef
        /// </summary>
        public ClefMode Clef { get; }

        /// <summary>
        ///     clef line
        /// </summary>
        public int ClefLine { get; }

        /// <summary>
        ///     transpose
        /// </summary>
        public ClefTranspose ClefTranspose { get; }

        /// <summary>
        ///     middle of the clef (if specified)
        /// </summary>
        public char Middle { get; }

        /// <summary>
        ///     playback transposition
        /// </summary>
        public int Transpose { get; }

        /// <summary>
        ///     octave transposition
        /// </summary>
        public int Octaves { get; }

        /// <summary>
        ///     number of staff lines
        /// </summary>
        public int Stafflines { get; }
    }
}
