using EmployeeManagementSystem.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.DAL.Configs
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired();
            builder.HasCheckConstraint("CK_Employee_Email_Format", "Email LIKE '%@%.%'");
            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasMany(e => e.Attendances).WithOne(a => a.Employee).HasForeignKey(a => a.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
