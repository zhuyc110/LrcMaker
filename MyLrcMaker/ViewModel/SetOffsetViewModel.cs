using MyLrcMaker.Infrastructure;

namespace MyLrcMaker.ViewModel
{
    public class SetOffsetViewModel : ResultViewModel<int>
    {
        public int Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }

        public override void Dispose()
        {
            Result = Offset;
        }

        #region Fields

        private int _offset;

        #endregion
    }
}