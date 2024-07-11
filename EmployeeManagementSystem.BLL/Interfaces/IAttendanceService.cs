using EmployeeManagementSystem.Models.Data;
using EmployeeManagementSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BLL.Interfaces
{
    public interface IAttendanceService : IService<Attendance>
    {
        public Task<List<Attendance>> GetByDate(DateTime date);
        public Task<List<Attendance>> GetByDateRangeAndDepartment(DateTime startDate, DateTime endDate,DepartmentEnum department);
        public Task<Attendance> CheckInOut(Attendance attendance);
        public Task<Attendance> GetAgainstEmployeeIdAndDate(Guid employeeId, DateTime date);
        public Task<List<Attendance>> GetAgainstEmployeeIdMonthAndYear(Guid employeeId, int month,int year);
    }
}
