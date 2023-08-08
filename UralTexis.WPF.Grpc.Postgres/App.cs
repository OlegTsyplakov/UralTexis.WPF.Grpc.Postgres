using Serilog;
using System.Windows;
using UralTexis.WPF.Grpc.Postgres;

namespace UralTexis.WPF
{
    public class App : Application
    {
        readonly MainWindow mainWindow;


        public App(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger.Information("Программа открыта");
            mainWindow.Show(); 
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Программа закрыта");
            base.OnExit(e);
        }
    }
}
