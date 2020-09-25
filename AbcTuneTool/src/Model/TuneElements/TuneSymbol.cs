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
        ///     symbol kind
        /// </summary>
        public DecorationSymbol Symbol { get; }
    }
}
