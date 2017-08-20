using System.ComponentModel.Composition;
using Prism.Mvvm;

namespace MyLrcMaker.Infrastructure
{
    [InheritedExport(typeof(IView<>))]
    public interface IView<in T> where T : BindableBase
    {
        T ViewModel { set; }
        string Title { get; }
    }
}