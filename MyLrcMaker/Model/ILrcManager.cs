using System.Collections.Generic;

namespace MyLrcMaker.Model
{
    public interface ILrcManager
    {
        IList<ILrcModel> LrcModels { get; }

        void LoadLrcFromInputString(string inputString, bool isFromFile = true);

        void SaveLrcToFile(string filePath, IList<ILrcModel> lrcModels);
    }
}