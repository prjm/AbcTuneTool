using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using AbcTuneTool.Common;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     terminal syntax part
    /// </summary>
    public class Terminal {

        /// <summary>
        ///     terminal values
        /// </summary>
        private ImmutableArray<Token> tokens;

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="tokenValues"></param>
        public Terminal(ImmutableArray<Token> tokenValues)
            => tokens = tokenValues;

        /// <summary>
        ///     check if a string is matched
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Matches(string input) {
            var tokenIndex = 0;
            var tokenCharIndex = 0;

            if (tokens.Length < 1)
                return false;

            if (input.Length < 1)
                throw new ArgumentOutOfRangeException();

            ref readonly var token = ref tokens.ItemRef(0);

            for (var charIndex = 0; charIndex < input.Length && tokenIndex < tokens.Length; charIndex++, tokenCharIndex++) {

                if (token.OriginalValue.Length <= tokenCharIndex) {
                    tokenCharIndex = 0;
                    tokenIndex++;

                    if (tokenIndex >= tokens.Length)
                        return false;

                    token = ref tokens.ItemRef(tokenIndex);
                }

                if (input[charIndex] != token.OriginalValue[tokenCharIndex])
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     add the content of this terminal to a string builder
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="target">target string builder</param>
        public void ToStringBuilder(int startPos, StringBuilder target) {
            var tokenCharIndex = 0;

            if (tokens.Length < 1)
                return;

            for (var tokenIndex = 0; tokenIndex < tokens.Length; tokenIndex++) {
                ref readonly var token = ref tokens.ItemRef(tokenIndex);

                for (var charIndex = 0; charIndex < token.Value.Length; charIndex++) {

                    if (tokenCharIndex >= startPos)
                        target.Append(token.Value[charIndex]);

                    tokenCharIndex++;
                }
            }
        }

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="token"></param>
        public Terminal(Token token)
            => tokens = ImmutableArray.Create(token);

        /// <summary>
        ///     create a new terminal
        /// </summary>
        /// <param name="values"></param>
        public Terminal(ObjectPoolItem<List<Token>> values)
            : this(values.ToImmutableArray()) { }

        /// <summary>
        ///     get the first char of this terminal
        /// </summary>
        public char FirstChar {
            get {
                if (tokens.Length < 1 || tokens[0].Value.Length < 1)
                    return '\0';
                return tokens[0].Value[0];
            }
        }

        /// <summary>
        ///     create a new string from the source tokens
        /// </summary>
        /// <returns></returns>
        public string ToNewString()
            => string.Concat(tokens);

    }
}