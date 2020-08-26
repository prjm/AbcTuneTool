using System.Collections.Generic;
using System.Collections.Immutable;
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