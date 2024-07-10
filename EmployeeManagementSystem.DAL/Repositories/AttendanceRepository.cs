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
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Attendance> GetAgainstEmployeeIdAndDate(Guid employeeId, DateTime date)
        {
            try
            {
                var result = await _context.Attendance.Include(a => a.Employee).Where(a => a.EmployeeId.Equals(employeeId) && a.Date.Date.Equals(date.Date)).FirstOrDefaultAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }               
    }
}
