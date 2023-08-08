using System.Collections.ObjectModel;
using UralTexis.WPF.Grpc.Postgres.Client;
using Serilog;
using UralTexis.GrpcService.Helpers;
using UralTexis.WPF.Models;
using System;


namespace UralTexis.WPF.ViewModels
{
    public class MainViewModel : IDisposable
    {
        private readonly GrpcClient _client;
        private bool disposedValue;
        private ObservableCollection<WorkerMessageDTO> _serverData = new();
        public ObservableCollection<WorkerMessageDTO> ServerData => _serverData;

        public MainViewModel()
        {
            _client = new GrpcClient();
            _client.OnReceived += OnServerReceivedEventHandler;
            _client.Startup();
        }


        void OnServerReceivedEventHandler(object sender, WorkerAction workerAction)
        {
            var workerMessageDTO = Mapper.WorkerActionToWorkerMessageDTO(workerAction);
            App.Current.Dispatcher.BeginInvoke(new System.Action(() => _serverData.Add(workerMessageDTO)));
            Log.Logger.Information($"OnTimeReceivedEventHandler: {workerAction.Worker.LastName}");

        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Shutdown();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {

            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
