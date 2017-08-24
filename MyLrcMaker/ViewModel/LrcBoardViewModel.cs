using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using MyLrcMaker.Extension;
using MyLrcMaker.Infrastructure;
using MyLrcMaker.Model;
using Prism.Commands;
using Prism.Mvvm;
using UiText = MyLrcMaker.Resources.UiTexts.MyLrcMaker;

namespace MyLrcMaker.ViewModel
{
    [Export(typeof(LrcBoardViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LrcBoardViewModel : BindableBase
    {
        public ICommand OpenLrcCommand { get; }

        public ICommand SaveLrcCommand { get; }

        public ICommand EditLrcCommand { get; }

        public ICommand LrcOffsetCommand { get; }

        public ICommand SetCurrentCommand { get; }

        public ObservableCollection<ILrcModel> LrcSource { get; set; }

        public ILrcModel SelectedLrcModel { get; set; }

        [ImportingConstructor]
        public LrcBoardViewModel(ILrcManager lrcManager, IIOService ioService, ISongService songService)
        {
            _lrcManager = lrcManager;
            _ioService = ioService;
            _songService = songService;
            LrcSource = new ObservableCollection<ILrcModel>(lrcManager.LrcModels);
            LrcSource.CollectionChanged += OnLrcCollectionChanged;

            OpenLrcCommand = new DelegateCommand(LoadLrc);
            SaveLrcCommand = new DelegateCommand(SaveLrc);
            EditLrcCommand = new DelegateCommand(EditLrc);
            LrcOffsetCommand = new DelegateCommand(OpenOffset, () => LrcSource.Any());
            SetCurrentCommand = new DelegateCommand(SetCurrent);
        }

        #region Private methods

        private void EditLrc()
        {
            _ioService.ShowDialog(new EditLrcViewModel(SelectedLrcModel, _songService), new DialogSetting {Height = 100, Width = 250});
        }

        private void ForceUpdateCanExecuteCommands()
        {
            LrcOffsetCommand.ForceUpdateCanExecuteCommand();
        }

        private void LoadLrc()
        {
            var file = _ioService.OpenFileDialog(UiText.OpenLrcDialog_Title, UiText.OpenSaveLrcDialog_Filter, false);
            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }
            _lrcManager.LoadLrcFromInputString(file);
            LrcSource.Clear();
            LrcSource.AddRange(_lrcManager.LrcModels);
        }

        private void OnLrcCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ForceUpdateCanExecuteCommands();
        }

        private void OpenOffset()
        {
            var viewModel = new SetOffsetViewModel();
            _ioService.ShowDialog(viewModel, new DialogSetting {Height = 65, Width = 100});
            if (viewModel.Offset != 0)
            {
                foreach (var lrcModel in LrcSource)
                {
                    lrcModel.Time += TimeSpan.FromSeconds(viewModel.Offset);
                }
            }
        }

        private void SaveLrc()
        {
            var file = _ioService.SaveFileDialog(UiText.SaveLrcDialog_Title, UiText.OpenSaveLrcDialog_Filter);
            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }
            _lrcManager.SaveLrcToFile(file, LrcSource);
        }

        private void SetCurrent()
        {
            if (SelectedLrcModel == null)
            {
                return;
            }

            SelectedLrcModel.Time = TimeSpan.FromMilliseconds(_songService.Current);
        }

        #endregion

        #region Fields

        private readonly ILrcManager _lrcManager;
        private readonly IIOService _ioService;
        private readonly ISongService _songService;

        #endregion
    }
}