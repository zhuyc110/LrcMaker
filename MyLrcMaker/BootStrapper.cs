using System.ComponentModel.Composition.Hosting;
using System.Windows;
using MyLrcMaker.View;
using Prism.Mef;
using Prism.Regions;

namespace MyLrcMaker
{
    internal class BootStrapper : MefBootstrapper
    {
        #region Protected methods

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BootStrapper).Assembly));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Container.GetExportedValue<IRegionManager>().RegisterViewWithRegion("MainRegion", typeof(MainView));
            Container.GetExportedValue<IRegionManager>().RegisterViewWithRegion("MainRegion", typeof(LrcBoardView));

            Application.Current.MainWindow.Show();
        }

        #endregion
    }
}