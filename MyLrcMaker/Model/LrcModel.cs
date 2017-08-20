using System;
using System.Text.RegularExpressions;
using MyLrcMaker.Extension;
using Prism.Mvvm;

namespace MyLrcMaker.Model
{
    internal class LrcModel : BindableBase, ILrcModel
    {
        public TimeSpan Time
        {
            get => _time;
            set
            {
                if (value != _time)
                {
                    SetProperty(ref _time, value);
                }
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    SetProperty(ref _text, value);
                }
            }
        }

        public LrcModel(string rawText)
        {
            if (_lineWithTimeRegex.IsMatch(rawText))
            {
                var matchedTexts = _lineWithTimeRegex.Match(rawText).Groups;
                Text = matchedTexts[4].Value;
                Time = new TimeSpan(0, 0, int.Parse(matchedTexts[1].Value), int.Parse(matchedTexts[2].Value), int.Parse(matchedTexts[3].Value.PadRight(3, '0')));
            }
            else
            {
                Text = rawText;
            }
        }

        public int CompareTo(LrcModel other)
        {
            return Time.CompareTo(other.Time);
        }

        public override string ToString()
        {
            return $"[{Time.ToLrcFormat()}]{Text}";
        }

        #region Fields

        private TimeSpan _time;
        private string _text;
        private readonly Regex _lineWithTimeRegex = new Regex(@"^\[(\d+):(\d+).(\d+)\]([\s\S]*)$");

        #endregion
    }
}