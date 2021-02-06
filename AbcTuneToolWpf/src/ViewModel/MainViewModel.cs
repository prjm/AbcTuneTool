using System.Collections.ObjectModel;

namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     main view model
    /// </summary>
    public class MainViewModel : BaseViewModel {

        /// <summary>
        ///     menu items
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
            = new ObservableCollection<MenuItemViewModel>();

        private TuneBookViewModel tuneBook = default!;
        public TuneBookViewModel TuneBook {
            get => tuneBook;
            set => SetProperty(ref tuneBook, value);
        }


        /// <summary>
        ///     create a new view model
        /// </summary>
        public MainViewModel() {
            TuneBook = new TuneBookViewModel();
            TuneBook.Tunes.Add(new TunesViewModel("1"));
            TuneBook.Tunes.Add(new TunesViewModel("2"));
            TuneBook.Tunes.Add(new TunesViewModel("3"));
            MenuItems.Add(CreateFileMenu());
        }

        private MenuItemViewModel CreateFileMenu() {
            var result = new MenuItemViewModel() {
                HeaderId = "test",
            };

            result.AddSubitem(new MenuItemViewModel() { HeaderId = "test" });
            result.AddSubitem(new MenuItemViewModel() { HeaderId = "test" });
            result.AddSubitem(new MenuItemViewModel() { HeaderId = "test" });

            return result;
        }
    }
}
