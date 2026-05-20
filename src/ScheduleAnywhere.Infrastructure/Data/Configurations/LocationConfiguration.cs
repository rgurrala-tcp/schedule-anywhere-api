using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> b)
    {
        b.ToTable("t_location");
        b.HasKey(l => l.Id);
        b.Property(l => l.Id).HasColumnName("id_i");
        b.Property(l => l.OrganizationId).HasColumnName("organization_id_i");
        b.Property(l => l.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(l => l.Description).HasColumnName("description_nvc");
        b.Property(l => l.Active).HasColumnName("active_b");
        b.Property(l => l.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(l => l.Organization).WithMany(o => o.Locations).HasForeignKey(l => l.OrganizationId);
    }
}
