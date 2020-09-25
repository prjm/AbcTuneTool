using System.Collections.Generic;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneTool.Model {

    /// <summary>
    ///     symbol shortcuts
    /// </summary>
    public class SymbolShortcuts {

        /// <summary>
        ///     registered shortcuts
        /// </summary>
        public Dictionary<char, DecorationSymbol>
            Shortcuts { get; } = new Dictionary<char, DecorationSymbol>();

        /// <summary>
        ///     symbol shortcuts
        /// </summary>
        public SymbolShortcuts()
            => ResetShortcuts();

        private void ResetShortcuts() {
            Shortcuts.Clear();
            Shortcuts.Add('~', DecorationSymbol.Roll);
            Shortcuts.Add('H', DecorationSymbol.Fermata);
            Shortcuts.Add('L', DecorationSymbol.Accent);
            Shortcuts.Add('M', DecorationSymbol.LowerMordent);
            Shortcuts.Add('O', DecorationSymbol.Coda);
            Shortcuts.Add('P', DecorationSymbol.UpperMordent);
            Shortcuts.Add('S', DecorationSymbol.Segno);
            Shortcuts.Add('T', DecorationSymbol.Trill);
            Shortcuts.Add('u', DecorationSymbol.Upbow);
            Shortcuts.Add('v', DecorationSymbol.Downbow);
        }
    }
}
