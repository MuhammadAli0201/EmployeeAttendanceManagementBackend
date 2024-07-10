using EmployeeManagementSystem.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.DAL.Configs
{
    internal class AttendenceConfig : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(a => new { a.EmployeeId, a.Date })
               .IsUnique();

            builder.HasCheckConstraint("CK_Attendance_CheckInBeforeCheckOut", "CheckInTime IS NULL OR CheckOutTime IS NULL OR CheckInTime < CheckOutTime");
        }
    }
}
