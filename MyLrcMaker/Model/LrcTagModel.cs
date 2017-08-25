using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace MyLrcMaker.Model
{
    public class LrcTagModel : BindableBase, ILrcTagModel
    {
        public TimeSpan Time { get; set; }

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

        public KeyValuePair<string, string> Tag { get; }

        public LrcTagModel(string rawText)
        {
            foreach (var supportTag in SupportTags)
            {
                var regString = $@"^\[({supportTag.Key}|{supportTag.Key.ToUpper()}):([\s\S]*)\]$";
                var reg = new Regex(regString);
                if (reg.IsMatch(rawText))
                {
                    Tag = supportTag;
                    Text = reg.Match(rawText).Groups[2].Value;
                }
            }
        }

        public override string ToString()
        {
            return $"{Tag.Value}: {Text}";
        }

        #region Fields

        private string _text;

        public static readonly Dictionary<string, string> SupportTags = new Dictionary<string, string>
        {
            {"ar", "艺术家"},
            {"ti", "标题"},
            {"al", "专辑"},
            {"by", "歌词编者"}
        };

        #endregion
    }
}