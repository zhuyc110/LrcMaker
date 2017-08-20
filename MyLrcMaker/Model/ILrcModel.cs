using System;

namespace MyLrcMaker.Model
{
    public interface ILrcModel
    {
        TimeSpan Time { get; set; }

        string Text { get; }
    }
}