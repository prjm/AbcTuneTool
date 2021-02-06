using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AbcTuneToolWpf.ViewModel {

    /// <summary>
    ///     base class for view model
    /// </summary>
    public abstract class BaseViewModel {

        /// <summary>
        ///     property changed event
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///     view model state
        /// </summary>
        public ViewModelState State { get; private set; } = ViewModelState.None;

        /// <summary>
        ///     raise a property changed event
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        ///     helper method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "") {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            if ((State & ViewModelState.Loading) != ViewModelState.Loading)
                State |= ViewModelState.Modified;

            return true;
        }


        /// <summary>
        ///     initialize this model
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public virtual Task<T> InitializeAsync<T>(T @this)
            => Task.FromResult(@this);

        /// <summary>
        ///     create a a new view model object
        /// </summary>
        /// <returns></returns>
        public static Task<T> CreateAsync<T>() where T : BaseViewModel, new() {
            var result = new T();
            return result.InitializeAsync(result);
        }


    }
}