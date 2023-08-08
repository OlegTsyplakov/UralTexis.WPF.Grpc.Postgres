using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UralTexis.Postgres.Models
{
    [Table("Employees", Schema = "Saratov")]
    public class Employee
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("FirstName")]
        public string? FirstName { get; set; }
        [Column("MiddleName")]
        public string? MiddleName { get; set; }
        [Column("LastName")]
        public string? LastName { get; set; }
        [Column("Birthday")]
        public int Birthday { get; set; }
        [Column("Sex")]
        public Sex Sex { get; set; }
        [Column("HaveChildren")]
        public bool HaveChildren { get; set; }
    }
}
