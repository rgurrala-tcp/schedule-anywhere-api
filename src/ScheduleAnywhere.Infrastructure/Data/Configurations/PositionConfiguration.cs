using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> b)
    {
        b.ToTable("t_position");
        b.HasKey(p => p.Id);
        b.Property(p => p.Id).HasColumnName("id_i");
        b.Property(p => p.OrganizationId).HasColumnName("organization_id_i");
        b.Property(p => p.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(p => p.Description).HasColumnName("description_nvc");
        b.Property(p => p.Active).HasColumnName("active_b");
        b.Property(p => p.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(p => p.Organization).WithMany(o => o.Positions).HasForeignKey(p => p.OrganizationId);
    }
}
