using System.Collections.Generic;
using System.Globalization;

namespace AbcTuneToolWpf.Other {

    public interface ITranslationProvider {

        IEnumerable<CultureInfo> Languages { get; }

        object? Translate(string key);
    }
}