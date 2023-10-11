using Banquet_Hall_App.Views;
using System.Windows;

namespace Banquet_Hall_App
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
