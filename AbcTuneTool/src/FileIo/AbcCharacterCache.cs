using System;
using System.Collections.Generic;
using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    internal struct AbcCharCacheKey : IEquatable<AbcCharCacheKey> {
        private readonly TokenKind kind;
        private readonly string originalValue;

        public AbcCharCacheKey(TokenKind kind, string originalValue) {
            this.kind = kind;
            this.originalValue = originalValue;
        }

        public override bool Equals(object? obj)
            => obj is AbcCharCacheKey key && Equals(key);

        public override int GetHashCode()
            => HashCode.Combine(kind, originalValue);

        public bool Equals(AbcCharCacheKey other)
            => other.kind == kind && string.Equals(other.originalValue, originalValue, StringComparison.Ordinal);
    }

    /// <summary>
    ///     character cache
    /// </summary>
    public class AbcCharacterCache {

        private readonly Dictionary<AbcCharCacheKey, AbcCharacterReference> cache
            = new Dictionary<AbcCharCacheKey, AbcCharacterReference>();

        /// <summary>
        ///     get a char reference from the cache
        /// </summary>
        /// <param name="value"></param>
        /// <param name="originalValue"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public AbcCharacterReference FromCache(string value, string originalValue, TokenKind kind) {
            var key = new AbcCharCacheKey(kind, originalValue);

            if (!cache.TryGetValue(key, out var result)) {
                result = new AbcCharacterReference(value, originalValue, kind);
                cache.Add(key, result);
            }

            return result;
        }

    }
}
