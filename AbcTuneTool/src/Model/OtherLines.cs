using System.Collections.Immutable;

namespace AbcTuneTool.Model {


    /// <summary>
    ///     other lines
    /// </summary>
    public class OtherLines {

        /// <summary>
        ///     create a new set of other lines
        /// </summary>
        /// <param name="lines"></param>
        public OtherLines(ImmutableArray<Token> lines)
            => Lines = lines;

        /// <summary>
        ///     lines
        /// </summary>
        public ImmutableArray<Token> Lines { get; }
    }
}
