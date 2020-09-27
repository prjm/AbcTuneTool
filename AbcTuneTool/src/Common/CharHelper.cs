using AbcTuneTool.Model;
using AbcTuneTool.Model.Symbolic;
using AbcTuneTool.Model.TuneElements;

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
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Accidental AsAccidental(this char c, bool onlyPrefix = false, Accidental defaultValue = Accidental.Invalid)
            => (c, onlyPrefix) switch
            {
                ('^', _) => Accidental.Sharp,
                ('_', _) => Accidental.Flat,
                ('=', _) => Accidental.Natural,
                ('#', false) => Accidental.Sharp,
                ('b', false) => Accidental.Flat,
                (' ', false) => Accidental.Undefined,
                ('\0', false) => Accidental.Undefined,
                _ => defaultValue
            };


        /// <summary>
        ///     convert a char to a position
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static AnnotationPosition AsPosition(this char c)
            => c switch
            {
                '@' => AnnotationPosition.Automatic,
                '<' => AnnotationPosition.Before,
                '>' => AnnotationPosition.After,
                '^' => AnnotationPosition.Above,
                '_' => AnnotationPosition.Below,
                _ => AnnotationPosition.Undefined,
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
        ///     test if a char can be a keynote letter
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsKeyNoteLetter(this char c) =>
            c >= 'A' && c <= 'G';

        /// <summary>
        ///     test if a char can be a decoration shortcut
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDecorationShortcut(this char c) =>
            c >= 'H' && c <= 'Z' ||
            c >= 'h' && c <= 'z' ||
            c == '~';

        /// <summary>
        ///     test if a char can be a note
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsNoteLetter(this char c) =>
            c >= 'A' && c < 'H' ||
            c >= 'a' && c < 'h';

    }
}
