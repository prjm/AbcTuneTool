using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace AbcTuneToolWpf.Other {

    public class TranslationManager {

        private static Lazy<TranslationManager> manager
            = new Lazy<TranslationManager>(() => new TranslationManager());

        public static TranslationManager Instance
            => manager.Value;

        public event EventHandler? LanguageChanged;

        public ITranslationProvider? TranslationProvider { get; set; }

        public IEnumerable<CultureInfo> Languages
            => TranslationProvider?.Languages ?? Enumerable.Empty<CultureInfo>();

        public CultureInfo CurrentLanguage {
            get => Thread.CurrentThread.CurrentUICulture;

            set {
                if (Thread.CurrentThread.CurrentUICulture == value)
                    return;

                Thread.CurrentThread.CurrentUICulture = value;
                OnLanguageChanged();
            }
        }

        protected void OnLanguageChanged() {
            if (LanguageChanged == default)
                return;

            LanguageChanged(this, EventArgs.Empty);
        }

        public object Translate(string key) {
            if (TranslationProvider != null) {
                var translatedValue = TranslationProvider.Translate(key);
                if (translatedValue != null) {
                    return translatedValue;
                }
            }

            return string.Format("!{0}!", key);
        }
    }
}
