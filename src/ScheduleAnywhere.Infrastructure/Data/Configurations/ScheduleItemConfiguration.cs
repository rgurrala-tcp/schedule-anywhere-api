using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class ScheduleItemConfiguration : IEntityTypeConfiguration<ScheduleItem>
{
    public void Configure(EntityTypeBuilder<ScheduleItem> b)
    {
        b.ToTable("t_scheduleItem");
        b.HasKey(s => s.Id);
        b.Property(s => s.Id).HasColumnName("id_i");
        b.Property(s => s.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(s => s.EmployeeId).HasColumnName("employee_id_i");
        b.Property(s => s.ShiftId).HasColumnName("shift_id_i");
        b.Property(s => s.ExplanationId).HasColumnName("explanation_id_i");
        b.Property(s => s.Date).HasColumnName("date_dt");
        b.Property(s => s.Notes).HasColumnName("notes_nvc").HasMaxLength(4000);
        b.Property(s => s.PaidHours).HasColumnName("paidhours_m").HasPrecision(8, 2);
        b.Property(s => s.UnpaidHours).HasColumnName("unpaidhours_m").HasPrecision(8, 2);
        b.Property(s => s.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(s => s.Schedule).WithMany(s => s.ScheduleItems).HasForeignKey(s => s.ScheduleId);
        b.HasOne(s => s.Employee).WithMany().HasForeignKey(s => s.EmployeeId);
        b.HasOne(s => s.Shift).WithMany().HasForeignKey(s => s.ShiftId);
        b.HasOne(s => s.Explanation).WithMany().HasForeignKey(s => s.ExplanationId);
    }
}
