using System.ComponentModel.Composition;

namespace MyLrcMaker.View
{
    /// <summary>
    /// SetOffsetView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(SetOffsetView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SetOffsetView
    {
        public override string Title => "偏移";

        public SetOffsetView()
        {
            InitializeComponent();
        }
    }
}