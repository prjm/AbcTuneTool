using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     view model for menus
    /// </summary>
    public class MenuItemViewModel {

        /// <summary>
        ///     header text
        /// </summary>
        public string Header { get; set; }
                = string.Empty;

        /// <summary>
        ///     menu command
        /// </summary>
        public ICommand? Command { get; set; }

        /// <summary>
        ///     sub items
        /// </summary>
        public ObservableCollection<MenuItemViewModel>? SubItems { get; set; }

        /// <summary>
        ///     add a menu sub item
        /// </summary>
        /// <param name="subitem"></param>
        public void AddSubitem(MenuItemViewModel subitem) {
            SubItems ??= new ObservableCollection<MenuItemViewModel>();
            SubItems.Add(subitem);
        }

    }
}
