namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     create a tune view model
    /// </summary>
    public class TunesViewModel : BaseViewModel {

        private string referenceNumber = string.Empty;

        /// <summary>
        ///     create a new tune
        /// </summary>
        /// <param name="refNumber"></param>
        public TunesViewModel(string refNumber)
            => ReferenceNumber = refNumber;

        /// <summary>
        ///     reference number
        /// </summary>
        public string ReferenceNumber {
            get => referenceNumber;
            set => SetProperty(ref referenceNumber, value);
        }
    }
}
