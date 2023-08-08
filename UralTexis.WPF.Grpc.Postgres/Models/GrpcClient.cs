using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Serilog;
using UralTexis.WPF.Settings;
using Microsoft.Extensions.Options;

namespace UralTexis.WPF.Grpc.Postgres.Client
{
    public class GrpcClient
    {
        private readonly WorkerIntegration.WorkerIntegrationClient? _client;
        private CancellationTokenSource? _cancellationTokenSource;
        private Task? _monitorTask;
        public event EventHandler<WorkerAction>? OnReceived;

        public GrpcClient()
        {
    
            var channel = GrpcChannel.ForAddress("http://localhost:5263");
            _client = new WorkerIntegration.WorkerIntegrationClient(channel);
        }

        public void Startup()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _monitorTask = Task.Run(() => MonitorServer(_cancellationTokenSource.Token));
        }

        public void Shutdown()
        {
            _cancellationTokenSource?.Cancel();
            _monitorTask?.Wait(10000);
        }

        private async Task MonitorServer(CancellationToken token)
        {
            try
            {
                var call = _client.GetAllWorkerStream(new EmptyMessage());
               if(call != null)
                {
                    while (await call.ResponseStream.MoveNext(token))
                    {
                        var result = call.ResponseStream.Current;
                        OnReceived?.Invoke(this, result);
                        Log.Logger.Information($"MonitorServer: {result.Worker.LastName}");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Logger.Warning($"Exception encountered in MonitorServer:{e.Message}");
            }
        }
    }
}
