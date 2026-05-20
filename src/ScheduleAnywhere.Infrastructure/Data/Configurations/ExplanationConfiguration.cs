using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class ExplanationConfiguration : IEntityTypeConfiguration<Explanation>
{
    public void Configure(EntityTypeBuilder<Explanation> b)
    {
        b.ToTable("t_explanation");
        b.HasKey(e => e.Id);
        b.Property(e => e.Id).HasColumnName("id_i");
        b.Property(e => e.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(e => e.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(e => e.Abbreviation).HasColumnName("abbreviation_nvc").HasMaxLength(20);
        b.Property(e => e.Description).HasColumnName("description_nvc");
        b.Property(e => e.Active).HasColumnName("active_b");
        b.Property(e => e.IsTimeOff).HasColumnName("istimeoff_b");
        b.Property(e => e.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(e => e.Schedule).WithMany(s => s.Explanations).HasForeignKey(e => e.ScheduleId);
    }
}
