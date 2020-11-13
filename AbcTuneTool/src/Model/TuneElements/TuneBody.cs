using System.Collections.Immutable;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     tune body
    /// </summary>
    public class TuneBody {

        /// <summary>
        ///     create a new tune body
        /// </summary>
        /// <param name="items"></param>
        public TuneBody(ImmutableArray<TuneElement> items)
            => Items = items;

        /// <summary>
        ///     tune items
        /// </summary>
        public ImmutableArray<TuneElement> Items { get; }
    }
}
