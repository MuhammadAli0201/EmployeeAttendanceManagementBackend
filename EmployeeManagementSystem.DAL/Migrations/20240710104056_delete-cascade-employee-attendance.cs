using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagementSystem.DAL.Migrations
{
    public partial class deletecascadeemployeeattendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Employee_EmployeeId",
                table: "Attendance");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_Email_Format",
                table: "Employee",
                sql: "Email LIKE '%@%.%'");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employee_EmployeeId",
                table: "Attendance",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Employee_EmployeeId",
                table: "Attendance");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_Email_Format",
                table: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employee_EmployeeId",
                table: "Attendance",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
