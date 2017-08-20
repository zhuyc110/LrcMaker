using System.Windows;
using System.Windows.Controls;

namespace MyLrcMaker.Infrastructure
{
    public abstract class DialogViewBase<TViewModel> : UserControl, IView<TViewModel> where TViewModel : ViewModelBase

    {
        public TViewModel ViewModel
        {
            set => DataContext = value;
            get => (TViewModel) DataContext;
        }

        public abstract string Title { get; }

        protected DialogViewBase()
        {
            Loaded += OnLoaded;
        }

        #region Private methods

        private void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }
            ViewModel?.Dispose();
            _isDisposed = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
            Dispose();
        }

        #endregion

        #region Fields

        private bool _isDisposed;

        #endregion
    }
}