namespace AbcTuneTool.Common {

    /// <summary>
    ///     char helper functions
    /// </summary>
    public static class CharHelper {

        /// <summary>
        ///     test if a char is a hexadecimal char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsHex(this char c)
            => c >= '0' && c <= '9' ||
                c >= 'a' && c <= 'f' ||
                c >= 'A' && c <= 'F';
    }
}
