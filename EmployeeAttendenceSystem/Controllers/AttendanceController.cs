using EmployeeManagementSystem.BLL.Interfaces;
using EmployeeManagementSystem.Models.Data;
using EmployeeManagementSystem.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendenceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("date")]
        public async Task<IActionResult> Index(DateTime date)
        {
            var attendances = await _attendanceService.GetByDate(date);
            return Ok(attendances);
        }

        [HttpPost(nameof(CheckInOut))]
        public async Task<IActionResult> CheckInOut(Attendance attendance)
        {
            var response = await _attendanceService.GetSingle(attendance.Id);

            //checks weather a employee attendence already exists if he try to add new attendence row.
            if (response != null && attendance.CheckInTime != null && response.CheckInTime != null) return Conflict("Already Checked In.");

            //handles the checkout if once done don't allow to checkout again against current date time.
            if (response != null && attendance.CheckOutTime != null && response?.CheckOutTime != null) return Conflict("Already Checked Out.");

            attendance = await _attendanceService.CheckInOut(attendance);
            return Ok(attendance);
        }

        [HttpGet(nameof(Report))]
        public async Task<IActionResult> Report(DateTime startDate, DateTime endDate, DepartmentEnum department)
        {
            var attendances = await _attendanceService.GetByDateRangeAndDepartment(startDate, endDate, department);
            return Ok(attendances);
        }
    }
}
