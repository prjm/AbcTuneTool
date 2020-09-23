using System.Collections.Immutable;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model.Fields {

    /// <summary>
    ///     symbol line field
    /// </summary>
    public class SymbolLineField : InformationField {

        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldHeader"></param>
        /// <param name="fieldValues"></param>
        /// <param name="symbols">symbol</param>
        public SymbolLineField(Terminal fieldHeader, Terminal fieldValues, ImmutableArray<TuneElement> symbols) : base(fieldHeader, fieldValues, InformationFieldKind.SymbolLine)
            => Elements = symbols.ToImmutableArray();

        /// <summary>
        ///     tune elements
        /// </summary>
        public ImmutableArray<TuneElement> Elements { get; }

    }
}
