using System;
using System.Collections.Generic;
using System.Text;

namespace AbcTuneTool.Common {

    internal class StringBuilderCacheEntry {
        private int hashCode;
        private string? stringData;
        private StringBuilder? stringBuilder;

        internal StringBuilderCacheEntry(string s) {
            stringData = s;
            var hc = new HashCode();
            for (var i = 0; i < s.Length; i++)
                hc.Add(s[i]);
            hashCode = hc.ToHashCode();
        }

        public void Initialize(StringBuilder s) {
            stringData = default;
            stringBuilder = s;
            var hc = new HashCode();
            for (var i = 0; i < s.Length; i++)
                hc.Add(s[i]);
            hashCode = hc.ToHashCode();
        }

        /// <summary>
        ///     string data
        /// </summary>
        public string Data => stringData!;

        public bool Equals(StringBuilderCacheEntry entry) {
            if (entry.GetHashCode() != hashCode)
                return false;

            if (!(entry.stringData is null) && !(stringData is null))
                return stringData.Equals(entry.stringData, StringComparison.Ordinal);

            if (!(entry.stringBuilder is null) && !(stringBuilder is null)) {
                if (entry.stringBuilder.Length != stringBuilder.Length)
                    return false;

                for (var i = 0; i < stringBuilder.Length; i++)
                    if (stringBuilder[i] != entry.stringBuilder[i])
                        return false;

                return true;
            }

            var sb = (stringBuilder ?? entry.stringBuilder)!;
            var s = (stringData ?? entry.stringData)!;

            if (s.Length != sb.Length)
                return false;

            for (var i = 0; i < s.Length; i++)
                if (s[i] != sb[i])
                    return false;

            return true;
        }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
            => obj is StringBuilderCacheEntry entry && Equals(entry);

        public override int GetHashCode()
            => hashCode;
    }


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
        ///     returns a cache string for string builder pool item
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string ForStringBuilder(ObjectPoolItem<StringBuilder> target)
            => ForStringBuilder(target.Item);

        private readonly Dictionary<(char, char, char, char), string> quadrupleChars
            = new Dictionary<(char, char, char, char), string>();

        private readonly HashSet<StringBuilderCacheEntry> cachedStrings
            = new HashSet<StringBuilderCacheEntry>();

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

        private readonly StringBuilderCacheEntry searchEntry
            = new StringBuilderCacheEntry(string.Empty);

        private string FromCache(StringBuilder sb) {
            searchEntry.Initialize(sb);
            if (cachedStrings.TryGetValue(searchEntry, out var cachedEntry))
                return cachedEntry.Data;

            cachedEntry = new StringBuilderCacheEntry(sb.ToString());
            cachedStrings.Add(cachedEntry);
            return cachedEntry.Data;
        }

        /// <summary>
        ///     get a string from a string builder
        /// </summary>
        /// <param name="stringBuilder">a string builder</param>
        /// <returns></returns>
        public string ForStringBuilder(StringBuilder stringBuilder)
            => stringBuilder.Length switch
            {
                0 => string.Empty,
                1 => ForChar(stringBuilder[0]),
                2 => ForChars(stringBuilder[0], stringBuilder[1]),
                3 => ForChars(stringBuilder[0], stringBuilder[1], stringBuilder[2]),
                4 => ForChars(stringBuilder[0], stringBuilder[1], stringBuilder[2], stringBuilder[3]),
                _ => FromCache(stringBuilder),
            };

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
        ///     cache a string for three chars
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

        /// <summary>
        ///     cache a string for four chars
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="c4"></param>
        /// <returns></returns>
        public string ForChars(char c1, char c2, char c3, char c4) {
            if (!quadrupleChars.TryGetValue((c1, c2, c3, c4), out var result)) {
                result = string.Concat(c1, c2, c3, c4);
                quadrupleChars.Add((c1, c2, c3, c4), result);
            }

            return result;
        }

        /// <summary>
        ///     add a char to a pooled string builder an get the cached string version
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ForStringBuilder(ObjectPoolItem<StringBuilder> sb, char value) {
            sb.Item.Append(value);
            return ForStringBuilder(sb.Item);
        }

        /// <summary>
        ///     add two chars to a pooled string builder an get the cached string
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <param name="lf"></param>
        /// <returns></returns>
        public string ForStringBuilder(ObjectPoolItem<StringBuilder> sb, char value, char lf) {
            sb.Item.Append(value);
            sb.Item.Append(lf);
            return ForStringBuilder(sb.Item);
        }
    }
}
