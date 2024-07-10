using EmployeeManagementSystem.BLL.Interfaces;
using EmployeeManagementSystem.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAttendenceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _employeeService.Get();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _employeeService.GetSingle(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(Employee employee)
        {
            if(employee.Id == Guid.Empty)
            {
                bool isEmailAlreadyExist = await _employeeService.IsEmailAvailable(employee.Email);
                if (isEmailAlreadyExist) return BadRequest("Email Already Exist.");
            }
            else
            {
                bool isEmailAlreadyExist = await _employeeService.IsEmailAvailableExcept(employee.Email,employee.Id);
                if (isEmailAlreadyExist) return BadRequest("Email Already Exist.");
            }

            employee = await _employeeService.CreateOrUpdate(employee);            
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _employeeService.GetSingle(id);
            if (employee == null) return NotFound();

            employee = await _employeeService.Delete(employee);
            return Ok(employee);
        }
    }
}
