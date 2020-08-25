using System;
using System.Diagnostics;
using AbcTuneTool.Model;

namespace AbcTuneTool.FileIo {

    /// <summary>
    ///     char reference
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AbcCharacterReference {


        /// <summary>
        ///     create a new char reference
        /// </summary>
        /// <param name="value"></param>
        /// <param name="originalValue"></param>
        /// <param name="kind"></param>
        public AbcCharacterReference(string value, string originalValue, TokenKind kind)
            => AbcChar = new Token(value, originalValue, kind);

        private Token token;

        /// <summary>
        ///     current token
        /// </summary>
        public ref Token AbcChar
            => ref token;

        internal string DebuggerDisplay
            => token.DebuggerDisplay;

        internal bool StartsWith(string aText)
            => token.OriginalValue.StartsWith(aText, StringComparison.Ordinal);

        internal string ExtractVersion() {
            var dashIndex = token.OriginalValue.IndexOf("-") + 1;
            if (dashIndex != KnownStrings.VersionComment.Length)
                return KnownStrings.UndefinedVersion;
            return token.OriginalValue.Substring(dashIndex);
        }
    }
}
