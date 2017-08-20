using System.ComponentModel.Composition;

namespace MyLrcMaker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(MainWindow))]
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}