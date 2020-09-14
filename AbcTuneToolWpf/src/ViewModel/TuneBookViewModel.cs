using System.Collections.ObjectModel;

namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     view model a tune book
    /// </summary>
    public class TuneBookViewModel {

        /// <summary>
        ///     set of tunes
        /// </summary>
        public ObservableCollection<TunesViewModel> Tunes { get; }
            = new ObservableCollection<TunesViewModel>();

    }
}
