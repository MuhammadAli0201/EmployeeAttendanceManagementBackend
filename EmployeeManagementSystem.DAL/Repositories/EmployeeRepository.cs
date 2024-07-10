using EmployeeManagementSystem.DAL.Interfaces;
using EmployeeManagementSystem.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee> GetByEmail(string email)
        {
            try
            {
                return await _context.Employee.FirstOrDefaultAsync(e => e.Email.Equals(email));
            }
            catch
            {
                throw;
            }
        }

        public async Task<Employee> GetByEmailExcept(string email, Guid id)
        {
            try
            {
                return await _context.Employee.Where(e => e.Id != id).FirstOrDefaultAsync(e => e.Email.Equals(email));
            }
            catch
            {
                throw;
            }
        }
    }
}
