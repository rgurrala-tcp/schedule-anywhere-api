using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class CoverageWatchConfiguration : IEntityTypeConfiguration<CoverageWatch>
{
    public void Configure(EntityTypeBuilder<CoverageWatch> b)
    {
        b.ToTable("t_coverageWatch");
        b.HasKey(c => c.Id);
        b.Property(c => c.Id).HasColumnName("id_i");
        b.Property(c => c.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(c => c.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(c => c.Active).HasColumnName("active_b");
        b.Property(c => c.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(c => c.Schedule).WithMany(s => s.CoverageWatches).HasForeignKey(c => c.ScheduleId);
    }
}

public class RequirementRowConfiguration : IEntityTypeConfiguration<RequirementRow>
{
    public void Configure(EntityTypeBuilder<RequirementRow> b)
    {
        b.ToTable("t_requirementRow");
        b.HasKey(r => r.Id);
        b.Property(r => r.Id).HasColumnName("id_i");
        b.Property(r => r.CoverageWatchId).HasColumnName("coveragewatch_id_i");
        b.Property(r => r.DayOfWeek).HasColumnName("dayofweek_i");
        b.Property(r => r.MinimumCount).HasColumnName("minimumcount_i");
        b.HasOne(r => r.CoverageWatch).WithMany(c => c.RequirementRows).HasForeignKey(r => r.CoverageWatchId);
    }
}
