using System.Collections.Generic;

namespace MyLrcMaker.Model
{
    public interface ILrcTagModel : ILrcModel
    {
        KeyValuePair<string, string> Tag { get; } 
    }
}