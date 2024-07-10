using EmployeeManagementSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DAL.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Task<Employee> GetByEmail(string email);
        public Task<Employee> GetByEmailExcept(string email, Guid id);
    }
}
