using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using UralTexis.Postgres.Interfaces;
using UralTexis.Postgres.Models;

namespace UralTexis.Postgres.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddEmployee(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _appDbContext.Employees.AddAsync(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public void DeleteEmployee(Employee employee)
        {
            _appDbContext.Employees.Remove(employee);
            _appDbContext.SaveChanges();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return _appDbContext.Employees;
        }

        public ObservableCollection<Employee> GetObservableCollectionEmployees()
        {
            return _appDbContext.Employees.Local.ToObservableCollection();
        }

        public void UpdateEmployee(Employee employee)
        {
            var entry = _appDbContext.Employees.Find(employee.Id);
            if (entry!=null)
            {
                _appDbContext.Entry(employee).State = EntityState.Modified;
                _appDbContext.SaveChanges();
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            var entry = _appDbContext.Employees.FindAsync(employee.Id);
            if (entry.IsCompletedSuccessfully)
            {
                _appDbContext.Entry(employee).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
