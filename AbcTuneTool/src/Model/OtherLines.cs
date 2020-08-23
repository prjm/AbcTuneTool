using System.Collections.Immutable;
using AbcTuneTool.FileIo;

namespace AbcTuneTool.Model {


    /// <summary>
    ///     other lines
    /// </summary>
    public class OtherLines {

        /// <summary>
        ///     create a new set of other lines
        /// </summary>
        /// <param name="lines"></param>
        public OtherLines(ImmutableArray<AbcCharacterReference> lines)
            => Lines = lines;

        /// <summary>
        ///     lines
        /// </summary>
        public ImmutableArray<AbcCharacterReference> Lines { get; }
    }
}
