namespace AbcTuneTool.Model {

    /// <summary>
    ///     accidental signs
    /// </summary>
    public enum Accidental {

        /// <summary>
        ///     undefined sign
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     flat
        /// </summary>
        Flat = 1,

        /// <summary>
        ///     sharp
        /// </summary>
        Sharp = 2,
    }

    /// <summary>
    ///     helper class for accidentals
    /// </summary>
    public static class AccidentalHelper {

        /// <summary>
        ///     convert this accidental to a char
        /// </summary>
        /// <param name="accidental"></param>
        /// <returns></returns>
        public static char AsChar(this Accidental accidental)
            => accidental switch
            {
                Accidental.Sharp => '#',
                Accidental.Flat => 'b',
                _ => ' '
            };

    }

}
