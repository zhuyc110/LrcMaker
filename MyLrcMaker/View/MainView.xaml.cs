using System.ComponentModel.Composition;
using MyLrcMaker.Infrastructure;
using MyLrcMaker.ViewModel;

namespace MyLrcMaker.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(MainView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainView : IView<MainViewModel>
    {
        [Import]
        public MainViewModel ViewModel
        {
            set => DataContext = value;
        }

        public string Title => "Main";

        [ImportingConstructor]
        public MainView(MainViewModel mainViewModel, ISongService songService)
        {
            InitializeComponent();
            songService.Initialize(MediaElementPlayer);
        }
    }
}