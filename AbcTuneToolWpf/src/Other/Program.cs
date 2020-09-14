using System;
using AbcTuneToolWpf.Views;

namespace AbcTuneToolWpf.Other {

    /// <summary>
    ///     main entry point
    /// </summary>
    public class Program {

        [STAThread]
        static void Main() {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

    }
}
