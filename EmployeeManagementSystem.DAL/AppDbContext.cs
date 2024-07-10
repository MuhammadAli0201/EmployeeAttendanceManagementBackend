using EmployeeManagementSystem.DAL.Configs;
using EmployeeManagementSystem.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new AttendenceConfig());
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
    }
}
