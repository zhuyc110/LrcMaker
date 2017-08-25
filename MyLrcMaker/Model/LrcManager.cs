using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;

namespace MyLrcMaker.Model
{
    [Export(typeof(ILrcManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LrcManager : ILrcManager
    {
        public IList<ILrcModel> LrcModels { get; }

        public LrcManager()
        {
            LrcModels = new List<ILrcModel>();
        }

        #region ILrcManager Members

        public void LoadLrcFromInputString(string inputString, bool isFromFile = true)
        {
            LrcModels.Clear();
            if (isFromFile)
            {
                if (File.Exists(inputString))
                {
                    LoadFromFile(inputString);
                }
                else
                {
                    MessageBox.Show("文件不存在");
                }
            }
            else
            {
                LoadFromContent(inputString);
            }
        }

        public void SaveLrcToFile(string filePath, IList<ILrcModel> lrcModels)
        {
            var content = string.Join(Environment.NewLine, lrcModels);
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                }
            }
        }

        #endregion

        #region Private methods

        private void LoadFromContent(string content)
        {
            var rawContent = content.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var line in rawContent)
            {
                CreateLrcModelFromRawText(line);
            }
        }

        private void LoadFromFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    var line = sr.ReadLine();
                    for (; line != null; line = sr.ReadLine())
                    {
                        CreateLrcModelFromRawText(line);
                    }
                }
            }
        }

        private void CreateLrcModelFromRawText(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            ILrcModel lrc;

            if (line.Trim().EndsWith("]"))
            {
                lrc = new LrcTagModel(line);
            }
            else
            {
                lrc = new LrcModel(line);
            }
            LrcModels.Add(lrc);
        }

        #endregion
    }
}