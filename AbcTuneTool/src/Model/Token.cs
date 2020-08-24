using System.Diagnostics;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     ABC character
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public readonly struct Token {

        /// <summary>
        ///     create a new ABC character
        /// </summary>
        /// <param name="kind">token kind</param>
        /// <param name="value">token value</param>
        /// <param name="originalValue">original value</param>
        public Token(string value, string originalValue, TokenKind kind) {
            Value = value;
            OriginalValue = originalValue;
            Kind = kind;
        }

        /// <summary>
        ///     character value
        /// </summary>
        public readonly string Value { get; }

        /// <summary>
        ///     original value
        /// </summary>
        public readonly string OriginalValue { get; }

        /// <summary>
        ///     character kind
        /// </summary>
        public readonly TokenKind Kind { get; }

        internal string DebuggerDisplay
            => $"{Kind}: {OriginalValue}";

    }
}
