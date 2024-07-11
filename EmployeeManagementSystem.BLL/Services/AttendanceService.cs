using EmployeeManagementSystem.BLL.Interfaces;
using EmployeeManagementSystem.DAL.Interfaces;
using EmployeeManagementSystem.Models.Data;
using EmployeeManagementSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BLL.Services
{
    public class AttendanceService : Service<Attendance>, IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AttendanceService(IAttendanceRepository repository,
            IEmployeeRepository employeeRepository
            ) : base(repository)
        {
            _attendanceRepository = repository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Attendance> CheckInOut(Attendance attendance)
        {
            if (attendance.Id == Guid.Empty)
            {
                attendance = await _attendanceRepository.Add(attendance);
            }
            else
            {
                attendance = await _attendanceRepository.Update(attendance);
            }
            return attendance;
        }

        public async Task<Attendance> GetAgainstEmployeeIdAndDate(Guid employeeId, DateTime date)
        {
            return await _attendanceRepository.GetAgainstEmployeeIdAndDate(employeeId, date);
        }

        public async Task<List<Attendance>> GetAgainstEmployeeIdMonthAndYear(Guid employeeId, int month, int year)
        {
            var employee = await _employeeRepository.GetSingle(employeeId);
            List<Attendance> attendancesAgainstAMonthAndYear = new List<Attendance>();
            if (employee != null)
            {
                DateTime date = new DateTime(year, month, 1);
                DateTime nextMonthDate = date.AddMonths(1);

                if(nextMonthDate.Month >= DateTime.Now.Month && nextMonthDate.Year >= DateTime.Now.Year)
                {
                    nextMonthDate = DateTime.Now;
                }

                while (date <= nextMonthDate)
                {
                    var response = await _attendanceRepository.GetAgainstEmployeeIdAndDate(employeeId, date);
                    if (response == null)
                    {
                        Attendance attendance = new Attendance
                        {
                            Date = date,
                            EmployeeId = employeeId,
                            Employee = employee

                        };
                        attendancesAgainstAMonthAndYear.Add(attendance);
                    }
                    else
                        attendancesAgainstAMonthAndYear.Add(response);
                    date = date.AddDays(1);
                }
            }
            return attendancesAgainstAMonthAndYear;

        }

        public async Task<List<Attendance>> GetByDate(DateTime date)
        {
            var employees = await _employeeRepository.Get();
            var attendences = new List<Attendance>();
            foreach (var employee in employees)
            {
                var attendanceAgainstDate = await _attendanceRepository.GetAgainstEmployeeIdAndDate(employee.Id, date);
                if (attendanceAgainstDate != null)
                {
                    attendanceAgainstDate.Employee = employee;
                    attendences.Add(attendanceAgainstDate);
                }
                else
                {
                    attendanceAgainstDate = new Attendance
                    {
                        Date = date,
                        EmployeeId = employee.Id,
                        Employee = employee,
                    };
                    attendences.Add(attendanceAgainstDate);
                }
            }
            return attendences.OrderBy(a => a.Employee.Name).ToList();
        }

        public async Task<List<Attendance>> GetByDateRangeAndDepartment(DateTime startDate, DateTime endDate, DepartmentEnum department)
        {
            DateTime date = startDate;
            var attendanceReport = new List<Attendance>();

            while (date <= endDate)
            {
                var attendanceOfDay = await GetByDate(date);
                var attendanceOfDayByDept = attendanceOfDay.Where(a => a.Employee.Department.Equals(department));
                attendanceReport.AddRange(attendanceOfDayByDept);
                date = date.AddDays(1);
            }
            return attendanceReport.OrderByDescending(a => a.Date).ToList();
        }
    }
}
