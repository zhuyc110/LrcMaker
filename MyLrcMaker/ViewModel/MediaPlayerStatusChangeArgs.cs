using System;

namespace MyLrcMaker.ViewModel
{
    public enum MediaPlayerCommand
    {
        Play,
        Pause,
        Stop
    }

    public class MediaPlayerStatusChangeArgs : EventArgs
    {
        public MediaPlayerCommand Command { get; }

        public MediaPlayerStatusChangeArgs(MediaPlayerCommand command)
        {
            Command = command;
        }
    }
}