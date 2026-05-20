using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class OpenShiftConfiguration : IEntityTypeConfiguration<OpenShift>
{
    public void Configure(EntityTypeBuilder<OpenShift> b)
    {
        b.ToTable("t_openShift");
        b.HasKey(o => o.Id);
        b.Property(o => o.Id).HasColumnName("id_i");
        b.Property(o => o.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(o => o.ShiftId).HasColumnName("shift_id_i");
        b.Property(o => o.Date).HasColumnName("date_dt");
        b.Property(o => o.IsActive).HasColumnName("isactive_b");
        b.Property(o => o.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(o => o.Schedule).WithMany(s => s.OpenShifts).HasForeignKey(o => o.ScheduleId);
        b.HasOne(o => o.Shift).WithMany().HasForeignKey(o => o.ShiftId);
    }
}

public class OpenShiftRequestConfiguration : IEntityTypeConfiguration<OpenShiftRequest>
{
    public void Configure(EntityTypeBuilder<OpenShiftRequest> b)
    {
        b.ToTable("t_openShiftRequest");
        b.HasKey(r => r.Id);
        b.Property(r => r.Id).HasColumnName("id_i");
        b.Property(r => r.OpenShiftId).HasColumnName("openshift_id_i");
        b.Property(r => r.EmployeeId).HasColumnName("employee_id_i");
        b.Property(r => r.IsApproved).HasColumnName("isapproved_b");
        b.Property(r => r.Notes).HasColumnName("notes_nvc");
        b.Property(r => r.RequestedAt).HasColumnName("requestedat_dt");
        b.Property(r => r.ReviewedAt).HasColumnName("reviewedat_dt");
        b.HasOne(r => r.OpenShift).WithMany(o => o.Requests).HasForeignKey(r => r.OpenShiftId);
        b.HasOne(r => r.Employee).WithMany().HasForeignKey(r => r.EmployeeId);
    }
}
