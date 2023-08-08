using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;
using System;
using System.IO;
using UralTexis.WPF.Grpc.Postgres;
using UralTexis.WPF.Settings;

namespace UralTexis.WPF
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

            var grpcUriString = configuration.GetSection("AppSettings:GrpcUri");
            var loggingSettings = configuration.GetSection("AppSettings:LogFilePath");
           

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .WriteTo.File(loggingSettings.Value, rollingInterval: RollingInterval.Hour, retainedFileCountLimit: null)
           .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.Configure<GrpcSettings>(grpcUriString);
                })
                .Build();

            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
