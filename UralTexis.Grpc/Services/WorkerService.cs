
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using UralTexis.Grpc;
using UralTexis.Grpc.Mapper;
using UralTexis.Postgres;
using UralTexis.Postgres.Interfaces;
using UralTexis.Postgres.Repository;
using static UralTexis.Grpc.WorkerIntegration;

namespace UralTexis.Grpc.Services
{
    public class WorkerService : WorkerIntegrationBase
    {
  
        private readonly IRepository _repository;
        private readonly Dictionary<Action, System.Func<WorkerAction, ServerCallContext, Task<EmptyMessage>>> _actionDictionary;
        public WorkerService(IRepository repository)
        {
            _repository = repository;
            _actionDictionary = new Dictionary<Action, System.Func<WorkerAction, ServerCallContext, Task<EmptyMessage>>>{
                    //{ Action.DefaultAction, },
                    { Action.Create, async (workerAction , serverCallContext) => await AddWorker(workerAction , serverCallContext) }, 
                    { Action.Update,async (workerAction , serverCallContext) => await UpdateWorker(workerAction , serverCallContext) },
                    { Action.Delete, async (workerAction , serverCallContext) => await DeleteWorker(workerAction , serverCallContext)  },

                };
        }

        public override async Task GetAllWorkerStream(EmptyMessage emptyMessage, IServerStreamWriter<WorkerAction> responseStream, ServerCallContext context)
        {
           
            var employees = _repository.GetObservableCollectionEmployees();
            foreach (var employee in employees)
            {

                await responseStream.WriteAsync(EmployeeMapper.EmployeeToWorkerAction(employee));

            }
        }
        public override async Task<EmptyMessage> AddWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.AddEmployeeAsync(EmployeeMapper.WorkerActionToEmployee(workerAction));
            return new EmptyMessage();
        }
        public override async Task<EmptyMessage> UpdateWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.UpdateEmployeeAsync(EmployeeMapper.WorkerActionToEmployee(workerAction));
            return new EmptyMessage();
        }
        public override async Task<EmptyMessage> DeleteWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.DeleteEmployeeAsync(EmployeeMapper.WorkerActionToEmployee(workerAction));
            return new EmptyMessage();
        }
        public override async Task<EmptyMessage> ChangeWorker(WorkerAction workerAction, ServerCallContext context)
        {
            return await _actionDictionary[workerAction.ActionType].Invoke(workerAction, context);
        }
    }
 
}
