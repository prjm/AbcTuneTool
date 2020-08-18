using System.Collections.Generic;
using System.Text;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     cache for allocated strings
    /// </summary>
    public class StringCache {

        private readonly Dictionary<char, string> singleChars
            = new Dictionary<char, string>();

        private readonly Dictionary<(char, char), string> doubleChars
            = new Dictionary<(char, char), string>();

        private readonly Dictionary<(char, char, char), string> tripleChars
            = new Dictionary<(char, char, char), string>();

        /// <summary>
        ///     get a string for a char
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public string ForChar(char c) {
            if (!singleChars.TryGetValue(c, out var result)) {
                result = new string(c, 1);
                singleChars.Add(c, result);
            }

            return result;
        }

        /// <summary>
        ///     get a string from a string builder
        /// </summary>
        /// <param name="stringBuilder">a string builder</param>
        /// <returns></returns>
        public string ForStringBuilder(StringBuilder stringBuilder)
            => stringBuilder.ToString();

        /// <summary>
        ///     cache a string for two chars
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public string ForChars(char c1, char c2) {
            if (!doubleChars.TryGetValue((c1, c2), out var result)) {
                result = string.Concat(c1, c2);
                doubleChars.Add((c1, c2), result);
            }

            return result;
        }


        /// <summary>
        ///     cache a string for two chars
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <returns></returns>
        public string ForChars(char c1, char c2, char c3) {
            if (!tripleChars.TryGetValue((c1, c2, c3), out var result)) {
                result = string.Concat(c1, c2, c3);
                tripleChars.Add((c1, c2, c3), result);
            }

            return result;
        }

    }
}
