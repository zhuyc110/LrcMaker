using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;

namespace MyLrcMaker.Infrastructure
{
    [Export(typeof(IIOService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class IOService : IIOService
    {
        [ImportingConstructor]
        public IOService(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        #region IIOService Members

        public void ShowDialog<TViewModel>(TViewModel viewModel, DialogSetting dialogSetting = null) where TViewModel : BindableBase
        {
            var view = _serviceLocator.GetInstance<IView<TViewModel>>();
            view.ViewModel = viewModel;
            ShowView(view, dialogSetting);
        }

        #endregion

        #region Private methods

        private static void ShowView<TViewModel>(IView<TViewModel> view, DialogSetting dialogSetting = null) where TViewModel : BindableBase
        {
            var window = new Window
            {
                Owner = Application.Current.MainWindow,
                Title = view.Title,
                Content = view,
                Width = dialogSetting?.Width ?? 200,
                Height = dialogSetting?.Height ?? 300,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        #endregion

        #region Fields

        private readonly IServiceLocator _serviceLocator;

        #endregion
    }
}