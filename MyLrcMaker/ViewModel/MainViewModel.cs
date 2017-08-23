using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using MyLrcMaker.Extension;
using MyLrcMaker.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;

namespace MyLrcMaker.ViewModel
{
    [Export(typeof(MainViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainViewModel : BindableBase
    {
        public LrcBoardViewModel LrcBoardViewModel { get; }

        public ICommand OpenMediaCommand { get; }

        public ICommand PlayMediaCommand { get; }

        public ICommand PauseMediaCommand { get; }

        public Uri MediaSource
        {
            get => _mediaSource;
            set
            {
                if (SetProperty(ref _mediaSource, value))
                {
                    ForceUpdateCanExecuteCommands();
                }
            }
        }

        public ISongService SongService { get; }

        [ImportingConstructor]
        public MainViewModel(LrcBoardViewModel lrcBoardViewModel, ISongService songService, IIOService ioService)
        {
            SongService = songService;
            _ioService = ioService;
            LrcBoardViewModel = lrcBoardViewModel;
            OpenMediaCommand = new DelegateCommand(OpenMediaFile);
            PlayMediaCommand = new DelegateCommand<MediaPlayerCommand?>(Play, CanPlay);
            PauseMediaCommand = new DelegateCommand<MediaPlayerCommand?>(Pause, CanPause);
        }

        #region Private methods

        private bool CanPause(MediaPlayerCommand? command)
        {
            return MediaSource != null && command.HasValue && IsMediaCommandLegal(command.Value);
        }

        private bool CanPlay(MediaPlayerCommand? command)
        {
            return MediaSource != null && command.HasValue && IsMediaCommandLegal(command.Value);
        }

        private void ForceUpdateCanExecuteCommands()
        {
            PlayMediaCommand.ForceUpdateCanExecuteCommand();
            PauseMediaCommand.ForceUpdateCanExecuteCommand();
        }

        private bool IsMediaCommandLegal(MediaPlayerCommand command)
        {
            if (_preCommand == MediaPlayerCommand.Play && command == MediaPlayerCommand.Play)
            {
                return false;
            }
            if ((_preCommand == MediaPlayerCommand.Pause || _preCommand == MediaPlayerCommand.Stop) && command == MediaPlayerCommand.Pause)
            {
                return false;
            }

            return true;
        }

        private void OpenMediaFile()
        {
            var file = _ioService.OpenFileDialog("打开音频文件", "音频文件|*.mp3;*.wav", false);
            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }

            MediaSource = new Uri(file);
        }

        private void Pause(MediaPlayerCommand? command)
        {
            SongService.Pause();
            RaiseMediaPlayerStatusChange(command);
        }

        private void Play(MediaPlayerCommand? command)
        {
            SongService.Play();
            RaiseMediaPlayerStatusChange(command);
        }

        private void RaiseMediaPlayerStatusChange(MediaPlayerCommand? command)
        {
            Debug.Assert(command != null, "command can not be null");
            _preCommand = command.Value;
            ForceUpdateCanExecuteCommands();
        }

        #endregion

        #region Fields

        private readonly IIOService _ioService;

        private Uri _mediaSource;
        private MediaPlayerCommand _preCommand = MediaPlayerCommand.Stop;

        #endregion
    }
}