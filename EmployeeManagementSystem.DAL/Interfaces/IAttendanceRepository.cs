using EmployeeManagementSystem.DAL.Repositories;
using EmployeeManagementSystem.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DAL.Interfaces
{
    public interface IAttendanceRepository:IRepository<Attendance>
    {
        public Task<Attendance> GetAgainstEmployeeIdAndDate(Guid employeeId,DateTime date);
    }
}
