using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace AbcTuneToolWpf.Other {

    public class TranslateExtension : MarkupExtension {

        public TranslateExtension(string key)
            => Key = key;

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var binding = new Binding("Value") {
                Source = new TranslationData(Key)
            };

            return binding.ProvideValue(serviceProvider);
        }
    }
}
