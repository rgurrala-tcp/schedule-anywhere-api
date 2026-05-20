using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> b)
    {
        b.ToTable("t_schedule");
        b.HasKey(s => s.Id);
        b.Property(s => s.Id).HasColumnName("id_i");
        b.Property(s => s.OrganizationId).HasColumnName("organization_id_i");
        b.Property(s => s.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(s => s.Description).HasColumnName("description_nvc");
        b.Property(s => s.Active).HasColumnName("active_b");
        b.Property(s => s.IsPosted).HasColumnName("isposted_b");
        b.Property(s => s.PostedDate).HasColumnName("posteddate_dt");
        b.Property(s => s.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(s => s.Organization).WithMany(o => o.Schedules).HasForeignKey(s => s.OrganizationId);
    }
}
