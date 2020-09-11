namespace AbcTuneTool.Model {

    /// <summary>
    ///     clef settings
    /// </summary>
    public class ClefSettings {

        /// <summary>
        ///     create new clef settings
        /// </summary>
        /// <param name="clef"></param>
        public ClefSettings(ClefMode clef)
            => Clef = clef;

        /// <summary>
        ///     clef
        /// </summary>
        public ClefMode Clef { get; }
    }
}
