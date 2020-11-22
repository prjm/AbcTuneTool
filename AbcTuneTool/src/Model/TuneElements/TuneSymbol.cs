using System;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     create a new tune symbol
    /// </summary>
    public class TuneSymbol : TuneElement {

        /// <summary>
        ///     create a new tune symbol
        /// </summary>
        /// <param name="symbol"></param>
        public TuneSymbol(DecorationSymbol symbol) => Symbol = symbol;

        /// <summary>
        ///     accept a visitor
        /// </summary>
        /// <param name="visitor"></param>
        public override bool Accept(ISyntaxTreeVisitor visitor) =>
            visitor.StartVisitNode(this) &&
            visitor.EndVisitNode(this);

        /// <summary>
        ///     symbol kind
        /// </summary>
        public DecorationSymbol Symbol { get; }

        /// <summary>
        ///     check for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(TuneElement? other)
            => other is TuneSymbol s && s?.Symbol == Symbol;

        /// <summary>
        ///     compute a hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => HashCode.Combine(Symbol.GetHashCode());
    }
}
