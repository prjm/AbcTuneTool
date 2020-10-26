using System.Windows;
using AbcTuneToolWpf.ViewModel;

namespace AbcTuneToolWpf.Views {

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected async override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            var mainWindow = new MainWindow() {
                DataContext = await BaseViewModel.CreateAsync<MainViewModel>()
            };
            mainWindow.Show();
        }
    }
}
