namespace AbcTuneTool.Model {

    /// <summary>
    ///     tone interval
    /// </summary>
    public enum ToneInterval {

        /// <summary>
        ///     undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     root
        /// </summary>
        Root = 1,

        /// <summary>
        ///     half step
        /// </summary>
        HalfStep = 2,

        /// <summary>
        ///     whole step
        /// </summary>
        WholeStep = 3,

    }

    /// <summary>
    ///     helper class
    /// </summary>
    public static class ToneIntervalHelper {

        /// <summary>
        ///     convert this interval to a number of half tones
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static int AsHalfTones(this ToneInterval interval)
            => interval switch
            {
                ToneInterval.HalfStep => 1,
                ToneInterval.WholeStep => 2,
                _ => 0
            };

    }

}
