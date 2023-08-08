using Riok.Mapperly.Abstractions;
using UralTexis.Postgres.Models;

namespace UralTexis.Grpc.Mapper
{
    [Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
    public static partial class EmployeeMapper
    {
   

        //[MapProperty(nameof(Employee), nameof(WorkerAction.Worker))]
        [MapperIgnoreSource(nameof(WorkerAction.ActionType))]
        public static partial Employee WorkerActionToEmployee(WorkerAction workerAction);

        //[MapProperty(nameof(Employee), nameof(WorkerAction.Worker))]
        [MapperIgnoreSource(nameof(Employee.Id))]
        public static partial WorkerAction EmployeeToWorkerAction(Employee employee);
    }
}
