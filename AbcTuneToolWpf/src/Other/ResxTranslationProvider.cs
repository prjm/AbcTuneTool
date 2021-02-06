using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace AbcTuneToolWpf.Other {

    public class ResxTranslationProvider : ITranslationProvider {

        private ResourceManager manager;

        public ResxTranslationProvider(string baseName, Assembly assembly)
            => manager = new ResourceManager(baseName, assembly);

        public IEnumerable<CultureInfo> Languages {
            get {
                yield return CultureInfo.GetCultureInfo("en-us");
            }
        }

        public object? Translate(string key)
            => manager.GetString(key);
    }
}
