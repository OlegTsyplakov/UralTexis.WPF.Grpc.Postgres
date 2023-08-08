using Mapster;
using UralTexis.WPF.Grpc.Postgres.Client;
using UralTexis.WPF.Models;

namespace UralTexis.GrpcService.Helpers
{
    public class Mapper
    {

        public static WorkerMessageDTO WorkerActionToWorkerMessageDTO(WorkerAction workerAction)
        {
            return workerAction.Worker.Adapt<WorkerMessageDTO>();
        }
        public static WorkerAction WorkerMessageDTOToWorkerAction(WorkerMessageDTO employee, Action actionType)
        {
            return new WorkerAction
            {
                Worker = employee.Adapt<WorkerMessage>(),
                ActionType = actionType
            };
        }
    }
}
