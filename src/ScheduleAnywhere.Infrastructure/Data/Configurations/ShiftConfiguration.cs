using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> b)
    {
        b.ToTable("t_shift");
        b.HasKey(s => s.Id);
        b.Property(s => s.Id).HasColumnName("id_i");
        b.Property(s => s.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(s => s.ColorId).HasColumnName("color_id_i");
        b.Property(s => s.ParentShiftId).HasColumnName("parentshift_id_i");
        b.Property(s => s.ShiftTagId).HasColumnName("shifttag_id_i");
        b.Property(s => s.TimeOffTagId).HasColumnName("timeofftag_id_i");
        b.Property(s => s.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(s => s.Abbreviation).HasColumnName("abbreviation_nvc").HasMaxLength(20);
        b.Property(s => s.Description).HasColumnName("description_nvc");
        b.Property(s => s.Code).HasColumnName("code_nvc").HasMaxLength(50);
        b.Property(s => s.Active).HasColumnName("active_b");
        b.Property(s => s.IsTimeOff).HasColumnName("istimeoff_b");
        b.Property(s => s.ShiftView).HasColumnName("shiftview_i");
        b.Property(s => s.StartTime).HasColumnName("starttime_dt");
        b.Property(s => s.StopTime).HasColumnName("stoptime_dt");
        b.Property(s => s.BreakMinutes).HasColumnName("breakminutes_i");
        b.Property(s => s.PaidHours).HasColumnName("paidhours_m").HasPrecision(8, 2);
        b.Property(s => s.UnpaidHours).HasColumnName("unpaidhours_m").HasPrecision(8, 2);
        b.Property(s => s.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(s => s.Schedule).WithMany(s => s.Shifts).HasForeignKey(s => s.ScheduleId);
        b.HasOne(s => s.Color).WithMany().HasForeignKey(s => s.ColorId);
        b.HasOne(s => s.ParentShift).WithMany(s => s.ChildShifts).HasForeignKey(s => s.ParentShiftId);
    }
}
