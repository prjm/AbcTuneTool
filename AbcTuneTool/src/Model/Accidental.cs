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

        /// <summary>
        ///     two flats
        /// </summary>
        DoubleFlat = 3,

        /// <summary>
        ///     two sharps
        /// </summary>
        DoubleSharp = 4,

        /// <summary>
        ///     natural
        /// </summary>
        Natural = 5,

        /// <summary>
        ///     invalid accidental
        /// </summary>
        Invalid = 6,
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
        public static string AsString(this Accidental accidental)
            => accidental switch
            {
                Accidental.Sharp => "♯",
                Accidental.Flat => "♭",
                Accidental.DoubleFlat => "𝄫",
                Accidental.DoubleSharp => "𝄪",
                Accidental.Natural => "♮",
                Accidental.Invalid => "☒",
                _ => string.Empty
            };

        public static Accidental Combine(this Accidental a1, Accidental a2)
            => (a1, a2) switch
            {
                (Accidental.Undefined, _) => a2,
                (_, Accidental.Undefined) => a1,
                (Accidental.Flat, Accidental.Flat) => Accidental.DoubleFlat,
                (Accidental.Flat, Accidental.Natural) => Accidental.Undefined,
                (Accidental.Sharp, Accidental.Sharp) => Accidental.DoubleSharp,
                (Accidental.Sharp, Accidental.Natural) => Accidental.Undefined,
                (Accidental.Natural, Accidental.Flat) => Accidental.Undefined,
                (Accidental.Natural, Accidental.Sharp) => Accidental.Undefined,
                _ => Accidental.Invalid,
            };

    }

}
