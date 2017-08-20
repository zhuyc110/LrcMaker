using System.ComponentModel.Composition;

namespace MyLrcMaker.View
{
    /// <summary>
    /// EditLrcView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(EditLrcView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class EditLrcView
    {
        public override string Title => "编辑时间";

        public EditLrcView()
        {
            InitializeComponent();
        }
    }
}