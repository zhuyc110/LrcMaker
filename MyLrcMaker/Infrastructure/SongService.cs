using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Prism.Mvvm;

namespace MyLrcMaker.Infrastructure
{
    [Export(typeof(ISongService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SongService : BindableBase, ISongService
    {
        public double TotalLength
        {
            get => _totalLength;
            set => SetProperty(ref _totalLength, value);
        }

        public double Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        #region ISongService Members

        public void Pause()
        {
            _mediaElement.Pause();

            if (_timer != null && _timer.IsEnabled)
            {
                _timer.Stop();
                Current = _mediaElement.Position.TotalMilliseconds;
            }
        }

        public void Initialize(IMediaElementHost mediaElement)
        {
            if (_isInitialized == false)
            {
                _isInitialized = true;
                _mediaElement = mediaElement;
                _mediaElement.MediaOpened += OnMediaOpened;
                _mediaElement.MediaEnded += OnMediaEnded;
            }
        }

        public void Play()
        {
            if (_timer != null && !_timer.IsEnabled)
            {
                _timer.Start();
            }
            _mediaElement.Play();
        }

        public void Release()
        {
            if (_timer != null && _timer.IsEnabled)
            {
                _timer.Stop();
                Current = 0;
            }
        }

        public void Resume()
        {
            if (_timer != null && !_timer.IsEnabled)
            {
                _timer.Start();
            }
        }

        #endregion

        #region Private methods

        private void OnMediaEnded(object sender, RoutedEventArgs e)
        {
            Release();
            Current = _mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void OnMediaOpened(object sender, RoutedEventArgs e)
        {
            Release();

            TotalLength = _mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            Current = _mediaElement.Position.TotalMilliseconds;
            _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, TimerTickCallBack, Dispatcher.CurrentDispatcher);
            _timer.Start();
        }

        private void TimerTickCallBack(object sender, EventArgs e)
        {
            Current = _mediaElement.Position.TotalMilliseconds;
        }

        #endregion

        #region Fields

        private IMediaElementHost _mediaElement;
        private DispatcherTimer _timer;
        private double _totalLength;
        private double _current;

        private bool _isInitialized = false;

        #endregion
    }
}