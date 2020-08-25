using System.Collections.Immutable;

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

        public string ToNewString()
            => string.Concat(tokens);

    }
}