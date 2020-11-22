using System.Collections.Immutable;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     tune body
    /// </summary>
    public class TuneBody : ISyntaxTreeElement {

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

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public bool Accept(ISyntaxTreeVisitor visitor) {
            var result = visitor.StartVisitNode(this);

            for (var i = 0; i < Items.Length; i++)
                result &= Items[i].Accept(visitor);

            result &= visitor.EndVisitNode(this);
            return result;
        }
    }
}
