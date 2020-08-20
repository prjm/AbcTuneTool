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
                value == '\x000B' ||
                value == '\x000C' ||
                value == '\x000D' ||
                value == '\x0085' ||
                value == '\x2028' ||
                value == '\x2029';

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
    }
}
