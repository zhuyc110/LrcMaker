using Prism.Mvvm;

namespace MyLrcMaker.Infrastructure
{
    public interface IIOService
    {
        void ShowDialog<TViewModel>(TViewModel viewModel, DialogSetting dialogSetting = null) where TViewModel : BindableBase;

        string OpenFileDialog(string title, string filter, bool multiselect);

        string SaveFileDialog(string title, string filter);
    }
}