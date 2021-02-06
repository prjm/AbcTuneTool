using System;
using System.ComponentModel;
using System.Windows;

namespace AbcTuneToolWpf.Other {

    public class TranslationData : IWeakEventListener, INotifyPropertyChanged, IDisposable {

        public TranslationData(string key)
            => Key = key;

        ~TranslationData() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                LanguageChangedEventManager.RemoveListener(TranslationManager.Instance, this);
            }
        }

        public object Value
            => TranslationManager.Instance.Translate(Key);

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e) {
            if (managerType != typeof(LanguageChangedEventManager))
                return false;

            OnLanguageChanged(sender, e);
            return true;
        }

        public string Key { get; }

        private void OnLanguageChanged(object sender, EventArgs e)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
