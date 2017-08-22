using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Mvvm;
using System.Windows.Forms;

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

        public string OpenFileDialog(string title, string filter, bool multiselect)
        {
            using (var ofd = new OpenFileDialog { Filter = filter, Multiselect = multiselect, Title = title })
            {
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }

            return string.Empty;
        }

        public string SaveFileDialog(string title, string filter)
        {
            using (var sfd = new SaveFileDialog { Filter = filter, Title = title })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    return sfd.FileName;
                }
            }

            return string.Empty;
        }

        #endregion

        #region Private methods

        private static void ShowView<TViewModel>(IView<TViewModel> view, DialogSetting dialogSetting = null) where TViewModel : BindableBase
        {
            var window = new Window
            {
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