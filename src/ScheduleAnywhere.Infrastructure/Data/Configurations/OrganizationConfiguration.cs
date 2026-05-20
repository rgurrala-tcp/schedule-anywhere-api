using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> b)
    {
        b.ToTable("t_organization");
        b.HasKey(o => o.Id);
        b.Property(o => o.Id).HasColumnName("id_i");
        b.Property(o => o.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(o => o.Code).HasColumnName("code_nvc").HasMaxLength(50);
        b.Property(o => o.Description).HasColumnName("description_nvc");
        b.Property(o => o.Status).HasColumnName("status_i");
        b.Property(o => o.Active).HasColumnName("active_b");
        b.Property(o => o.Created).HasColumnName("created_dt");
        b.Property(o => o.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
    }
}
