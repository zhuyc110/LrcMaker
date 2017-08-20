using System;
using System.Windows;

namespace MyLrcMaker.Infrastructure
{
    public interface IMediaElementHost
    {
        event RoutedEventHandler MediaEnded;

        event RoutedEventHandler MediaOpened;

        TimeSpan Position { get; }

        Duration NaturalDuration { get; }

        void Pause();

        void Play();

        void Stop();
    }
}