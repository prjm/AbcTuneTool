using System.Collections.Immutable;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     part group
    /// </summary>
    public class PartGroup : PartItem {

        /// <summary>
        ///     create a new part group
        /// </summary>
        /// <param name="immutableArrays"></param>
        public PartGroup(ImmutableArray<PartItem> immutableArrays)
            => Items = immutableArrays;

        /// <summary>
        ///     part items
        /// </summary>
        public ImmutableArray<PartItem> Items { get; }

    }
}