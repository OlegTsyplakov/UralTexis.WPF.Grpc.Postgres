using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UralTexis.Postgres.Models;

namespace UralTexis.Postgres.Interfaces
{
    public interface IRepository
    {
        Task AddEmployeeAsync(Employee employee);
        void AddEmployee(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        void UpdateEmployee(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        void DeleteEmployee(Employee employee);
        IQueryable<Employee> GetAllEmployees();
        ObservableCollection<Employee> GetObservableCollectionEmployees();
    }
}
