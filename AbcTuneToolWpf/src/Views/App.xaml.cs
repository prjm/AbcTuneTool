using System.Reflection;
using System.Windows;

using AbcTuneToolWpf.Other;
using AbcTuneToolWpf.ViewModel;

namespace AbcTuneToolWpf.Views {

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override async void OnStartup(StartupEventArgs e) {

            const string BaseName = "AbcTuneToolWpf.data.res.Resources";
            TranslationManager.Instance.TranslationProvider = new ResxTranslationProvider(BaseName, Assembly.GetExecutingAssembly());

            base.OnStartup(e);
            var mainWindow = new MainWindow() {
                DataContext = await BaseViewModel.CreateAsync<MainViewModel>()
            };
            mainWindow.Show();
        }
    }
}
