using Mapster;
using UralTexis.Grpc;
using UralTexis.Postgres.Models;
using Action = UralTexis.Grpc.Action;

namespace UralTexis.GrpcService.Helpers
{
    public class Mapper
    {

        public static Employee WorkerActionToEmployee(WorkerAction workerAction)
        {
            return workerAction.Worker.Adapt<Employee>();
        }
        public static WorkerAction EmployeeToWorkerAction(Employee employee, Action actionType)
        {
            return new WorkerAction
            {
                Worker = employee.Adapt<WorkerMessage>(),
                ActionType = actionType
            };
        }
    }
}
