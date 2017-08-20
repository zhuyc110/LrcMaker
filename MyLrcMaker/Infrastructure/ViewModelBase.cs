using Prism.Mvvm;

namespace MyLrcMaker.Infrastructure
{
    public abstract class ViewModelBase : BindableBase
    {
        public virtual void Dispose()
        {
        }
    }
}