namespace AbcTuneTool.Model {

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
        public ClefSettings(ClefMode clef, int clefLine, ClefTranspose clefTranspose) {
            Clef = clef;
            ClefLine = clefLine;
            ClefTranspose = clefTranspose;
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
    }
}
