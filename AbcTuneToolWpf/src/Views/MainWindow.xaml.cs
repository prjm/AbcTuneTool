using System.Windows;
using AbcTuneToolWpf.ViewModel;

namespace AbcTuneToolWpf {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
