namespace MyLrcMaker.Infrastructure
{
    public abstract class ResultViewModel<T> : ViewModelBase
    {
        public T Result { get; protected set; }
    }
}