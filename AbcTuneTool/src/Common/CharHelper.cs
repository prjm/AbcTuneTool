using AbcTuneTool.Model;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     char helper functions
    /// </summary>
    public static class CharHelper {

        /// <summary>
        ///     test if a char is a hexadecimal char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsHex(this char value) =>
                value >= '0' && value <= '9' ||
                value >= 'a' && value <= 'f' ||
                value >= 'A' && value <= 'F';

        /// <summary>
        ///     test if a char is a line break char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLinebreak(this char value) =>
                value == '\x000A' ||
                value == '\x000C' ||
                value == '\x000D' ||
                value == '\x0085' ||
                value == '\x2028' ||
                value == '\x2029';


        /// <summary>
        ///     test if a char is an ASCII letter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsAsciiLetter(this char value) =>
                value >= 'A' && value <= 'Z' ||
                value >= 'a' && value <= 'z';

        /// <summary>
        ///     test if a char is a numeric value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(this char value) =>
                value >= '0' && value <= '9';

        /// <summary>
        ///     test if a char is a simple whitespace char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsWhitespace(this char value) =>
                value == ' ' || value == '\t' || value == '\v' || value.IsLinebreak();

        /// <summary>
        ///     test if a char is a carriage return char
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsCr(this char value) =>
                value == '\x000D';

        /// <summary>
        ///     test if a char is a linefeed character
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLf(this char value) =>
                value == '\x000A';

        /// <summary>
        ///     test if a char marks a header field
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool MarksInfoField(this char value) =>
                value == 'A' ||
                value == 'B' ||
                value == 'C' ||
                value == 'D' ||
                value == 'F' ||
                value == 'G' ||
                value == 'H' ||
                value == 'I' ||
                value == 'K' ||
                value == 'L' ||
                value == 'M' ||
                value == 'm' ||
                value == 'N' ||
                value == 'O' ||
                value == 'P' ||
                value == 'R' ||
                value == 'r' ||
                value == 'S' ||
                value == 's' ||
                value == 'T' ||
                value == 'U' ||
                value == 'V' ||
                value == 'W' ||
                value == 'w' ||
                value == 'X' ||
                value == 'Z' ||
                value == '+';

        /// <summary>
        ///     get the accidental for a char
        /// </summary>
        /// <param name="c"></param>
        /// <param name="onlyPrefix"></param>
        /// <returns></returns>
        public static Accidental AsAccidental(this char c, bool onlyPrefix = false)
            => (c, onlyPrefix) switch
            {
                ('^', _) => Accidental.Sharp,
                ('_', _) => Accidental.Flat,
                ('=', _) => Accidental.Natural,
                ('#', false) => Accidental.Sharp,
                ('b', false) => Accidental.Flat,
                (' ', false) => Accidental.Undefined,
                ('\0', false) => Accidental.Undefined,
                _ => Accidental.Invalid
            };

        /// <summary>
        ///     get the accidental for a char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static ToneInterval AsToneInterval(this char c)
            => c switch
            {
                'R' => ToneInterval.Root,
                'H' => ToneInterval.HalfStep,
                'W' => ToneInterval.WholeStep,
                _ => ToneInterval.Undefined
            };


        /// <summary>
        ///
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsKeyNoteLetter(this char c) =>
            c >= 'A' && c <= 'G';

    }
}
