using EmployeeManagementSystem.BLL.Interfaces;
using EmployeeManagementSystem.DAL.Interfaces;
using EmployeeManagementSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BLL.Services
{
    public class EmployeeService : Service<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            _employeeRepository = repository;
        }

        public async Task<Employee> CreateOrUpdate(Employee employee)
        {
            if (employee.Id == Guid.Empty)
                employee = await _employeeRepository.Add(employee);
            else
                employee = await _employeeRepository.Update(employee);

            return employee;
        }

        public async Task<bool> IsEmailAvailable(string email)
        {
            var result = await _employeeRepository.GetByEmail(email);
            if (result != null)
                return true;
            else
                return false;
        }

        public async Task<bool> IsEmailAvailableExcept(string email, Guid id)
        {
            var result = await _employeeRepository.GetByEmailExcept(email,id);
            if (result != null)
                return true;
            else
                return false;
        }
    }
}
