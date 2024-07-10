using EmployeeManagementSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models.Data
{
    public class Employee
    {
        public Guid Id { get; set; }
        [MinLength(2)]
        public string Name { get; set; }
        public string Email { get; set; }
        public DepartmentEnum Department { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
}
