using System.ComponentModel;

namespace MyLrcMaker.Infrastructure
{
    public interface ISongService : INotifyPropertyChanged
    {
        double TotalLength { get; }

        double Current { get; }

        void Initialize(IMediaElementHost mediaElement);

        void Play();

        void Pause();

        void Resume();

        void Release();
    }
}