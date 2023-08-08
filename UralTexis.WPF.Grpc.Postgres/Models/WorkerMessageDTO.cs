using UralTexis.WPF.Grpc.Postgres.Client;

namespace UralTexis.WPF.Models
{
    public class WorkerMessageDTO
    {

        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Birthday { get; set; }
        public Sex Sex { get; set; }
        public bool HaveChildren { get; set; }
    }

}

