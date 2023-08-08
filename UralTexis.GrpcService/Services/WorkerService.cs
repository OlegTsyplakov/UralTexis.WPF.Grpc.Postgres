using Grpc.Core;
using UralTexis.GrpcService.Helpers;
using UralTexis.Postgres.Interfaces;
using static UralTexis.Grpc.WorkerIntegration;

namespace UralTexis.Grpc.Services
{
    public class WorkerService : WorkerIntegrationBase
    {
  
        private readonly IRepository _repository;
        private readonly Dictionary<Action, System.Func<WorkerAction,  ServerCallContext, Task<WorkerAction>>> _actionDictionary;
        public WorkerService(IRepository repository)
        {
            _repository = repository;
            _actionDictionary = new Dictionary<Action, System.Func<WorkerAction, ServerCallContext, Task<WorkerAction>>>{
                    { Action.Create, async (workerAction , serverCallContext) => await AddWorker(workerAction,  serverCallContext) }, 
                    { Action.Update,async (workerAction , serverCallContext) => await UpdateWorker(workerAction, serverCallContext) },
                    { Action.Delete, async (workerAction , serverCallContext) => await DeleteWorker(workerAction, serverCallContext)  },

                };
        }

        public override async Task GetAllWorkerStream(EmptyMessage emptyMessage, IServerStreamWriter<WorkerAction> responseStream, ServerCallContext context)
        {
            var employees = _repository.GetAllEmployees();
            foreach (var employee in employees)
            {
                await responseStream.WriteAsync(Mapper.EmployeeToWorkerAction(employee, Action.DefaultAction));
            }
        }
        async Task<WorkerAction> AddWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.AddEmployeeAsync(Mapper.WorkerActionToEmployee(workerAction));
            return workerAction;
        }
        async Task<WorkerAction> UpdateWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.UpdateEmployeeAsync(Mapper.WorkerActionToEmployee(workerAction));
            return workerAction;
        }
        async Task<WorkerAction> DeleteWorker(WorkerAction workerAction, ServerCallContext context)
        {
            await _repository.DeleteEmployeeAsync(Mapper.WorkerActionToEmployee(workerAction));
            return workerAction;
        }
        public override async Task<WorkerAction> ChangeWorker(WorkerAction workerAction, ServerCallContext context)
        {
            return await _actionDictionary[workerAction.ActionType].Invoke(workerAction, context);
        }
    }
 
}
