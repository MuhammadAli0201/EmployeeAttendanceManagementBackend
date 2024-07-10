using EmployeeManagementSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BLL.Interfaces
{
    public interface IEmployeeService : IService<Employee>
    {
        public Task<Employee> CreateOrUpdate(Employee employee);
        public Task<bool> IsEmailAvailable(string email);
    }
}
