using System.ComponentModel.Composition;
using MyLrcMaker.Infrastructure;
using MyLrcMaker.ViewModel;

namespace MyLrcMaker.View
{
    /// <summary>
    /// LrcBoardView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(LrcBoardView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class LrcBoardView : IView<LrcBoardViewModel>
    {
        [Import]
        public LrcBoardViewModel ViewModel
        {
            set => DataContext = value;
        }

        public string Title => "Lrc board";

        public LrcBoardView()
        {
            InitializeComponent();
        }
    }
}