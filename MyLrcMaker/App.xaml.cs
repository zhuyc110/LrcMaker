using System.Windows;

namespace MyLrcMaker
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region Protected methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootStrapper = new BootStrapper();
            bootStrapper.Run();
        }

        #endregion
    }
}