using System;

namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     view model state
    /// </summary>
    [Flags]
    public enum ViewModelState {


        None = 0b_00,

        Loading = 0b_01,

        Modified = 0b_10,



    }
}
