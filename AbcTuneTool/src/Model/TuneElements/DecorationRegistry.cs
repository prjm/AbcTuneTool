using System.Collections.Generic;

namespace AbcTuneTool.Model.TuneElements {

    /// <summary>
    ///     decoration symbols
    /// </summary>
    public class DecorationRegistry {

        /// <summary>
        ///     create a new decoration registry
        /// </summary>
        public DecorationRegistry() {
            Symbols = new Dictionary<string, DecorationSymbol>();
            AddSymbols();
        }

        /// <summary>
        ///     registered symbols
        /// </summary>
        public Dictionary<string, DecorationSymbol> Symbols { get; }

        private void AddSymbol(string symbol, DecorationSymbol kind)
            => Symbols.Add(symbol, kind);

        private void AddSymbols() {
            AddSymbol("trill", DecorationSymbol.Trill);
            AddSymbol("trill(", DecorationSymbol.TrillStart);
            AddSymbol("trill)", DecorationSymbol.TrillEnd);
            AddSymbol("lowermordent", DecorationSymbol.LowerMordent);
            AddSymbol("uppermordent", DecorationSymbol.UpperMordent);
            AddSymbol("mordent", DecorationSymbol.Mordent);
            AddSymbol("pralltriller", DecorationSymbol.PrallTriller);
            AddSymbol("roll", DecorationSymbol.Roll);
            AddSymbol("turn", DecorationSymbol.Turn);
            AddSymbol("turnx", DecorationSymbol.TurnX);
            AddSymbol("invertedturn", DecorationSymbol.Invertedturn);
            AddSymbol("invertedturnx", DecorationSymbol.InvertedturnX);
            AddSymbol("arpeggio", DecorationSymbol.Arpeggio);
            AddSymbol(">", DecorationSymbol.Gt);
            AddSymbol("accent", DecorationSymbol.Accent);
            AddSymbol("emphasis", DecorationSymbol.Emphasis);
            AddSymbol("fermata", DecorationSymbol.Fermata);
            AddSymbol("invertedfermata", DecorationSymbol.InvertedFermata);
            AddSymbol("tenuto", DecorationSymbol.Tenuto);
            AddSymbol("0", DecorationSymbol.Finger0);
            AddSymbol("1", DecorationSymbol.Finger1);
            AddSymbol("2", DecorationSymbol.Finger2);
            AddSymbol("3", DecorationSymbol.Finger3);
            AddSymbol("4", DecorationSymbol.Finger4);
            AddSymbol("5", DecorationSymbol.Finger5);
            AddSymbol("+", DecorationSymbol.Add);
            AddSymbol("plus", DecorationSymbol.Plus);
            AddSymbol("snap", DecorationSymbol.Snap);
            AddSymbol("slide", DecorationSymbol.Slide);
            AddSymbol("wedge", DecorationSymbol.Wedge);
            AddSymbol("upbow", DecorationSymbol.Upbow);
            AddSymbol("downbow", DecorationSymbol.Downbow);
            AddSymbol("open", DecorationSymbol.Open);
            AddSymbol("thumb", DecorationSymbol.Thumb);
            AddSymbol("breath", DecorationSymbol.Breath);
            AddSymbol("pppp", DecorationSymbol.PpppSymbol);
            AddSymbol("ppp", DecorationSymbol.PppSymbol);
            AddSymbol("pp", DecorationSymbol.PpSymbol);
            AddSymbol("p", DecorationSymbol.PSymbol);
            AddSymbol("mp", DecorationSymbol.MpSymbol);
            AddSymbol("mf", DecorationSymbol.MfSymbol);
            AddSymbol("f", DecorationSymbol.FSymbol);
            AddSymbol("ff", DecorationSymbol.FfSymbol);
            AddSymbol("fff", DecorationSymbol.FffSymbol);
            AddSymbol("ffff", DecorationSymbol.FfffSymbol);
            AddSymbol("sfz", DecorationSymbol.SfzSymbol);
            AddSymbol("crescendo(", DecorationSymbol.CrescendoStart);
            AddSymbol("<(", DecorationSymbol.CrescendStartShort);
            AddSymbol("crescendo)", DecorationSymbol.CrescendoEnd);
            AddSymbol("<)", DecorationSymbol.CrescendoEndShort);
            AddSymbol("diminuendo(", DecorationSymbol.DiminuendoStart);
            AddSymbol(">(", DecorationSymbol.DiminuendoStartShort);
            AddSymbol("diminuendo)", DecorationSymbol.DiminuendoEnd);
            AddSymbol(">)", DecorationSymbol.DiminuendoEndShort);
            AddSymbol("segno", DecorationSymbol.Segno);
            AddSymbol("coda", DecorationSymbol.Coda);
            AddSymbol("D.S.", DecorationSymbol.Ds);
            AddSymbol("D.C.", DecorationSymbol.Dc);
            AddSymbol("dacoda", DecorationSymbol.Dacoda);
            AddSymbol("dacapo", DecorationSymbol.Dacapo);
            AddSymbol("fine", DecorationSymbol.Fine);
            AddSymbol("shortphrase", DecorationSymbol.Shortphrase);
            AddSymbol("mediumphrase", DecorationSymbol.Mediumphrase);
            AddSymbol("longphrase", DecorationSymbol.Longphrase);
        }

    }
}
