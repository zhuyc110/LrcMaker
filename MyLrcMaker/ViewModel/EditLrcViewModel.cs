using System;
using System.Windows.Input;
using MyLrcMaker.Infrastructure;
using MyLrcMaker.Model;
using Prism.Commands;

namespace MyLrcMaker.ViewModel
{
    public class EditLrcViewModel : ResultViewModel<ILrcModel>
    {
        private readonly ISongService _songService;
        public ILrcModel LrcModel { get; }

        public ICommand GetCurrentTimeCommand { get; }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        public int Milliseconds
        {
            get => _milliseconds;
            set => SetProperty(ref _milliseconds, value);
        }

        public EditLrcViewModel(ILrcModel lrcModel, ISongService songService)
        {
            _songService = songService;
            LrcModel = lrcModel;
            Minutes = LrcModel.Time.Minutes;
            Seconds = LrcModel.Time.Seconds;
            Milliseconds = LrcModel.Time.Milliseconds;
            GetCurrentTimeCommand = new DelegateCommand(SetCurrentTimeToLrcTime);
        }

        private void SetCurrentTimeToLrcTime()
        {
            var time = TimeSpan.FromMilliseconds(_songService.Current);
            Minutes = time.Minutes;
            Seconds = time.Seconds;
            Milliseconds = time.Milliseconds;
        }

        public override void Dispose()
        {
            LrcModel.Time = new TimeSpan(0, 0, Minutes, Seconds, Milliseconds);
            Result = LrcModel;
        }

        #region Fields

        private int _minutes;
        private int _seconds;
        private int _milliseconds;

        #endregion
    }
}