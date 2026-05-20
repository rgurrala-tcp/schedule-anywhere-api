using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> b)
    {
        b.ToTable("t_department");
        b.HasKey(d => d.Id);
        b.Property(d => d.Id).HasColumnName("id_i");
        b.Property(d => d.OrganizationId).HasColumnName("organization_id_i");
        b.Property(d => d.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(d => d.Description).HasColumnName("description_nvc");
        b.Property(d => d.Active).HasColumnName("active_b");
        b.Property(d => d.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(d => d.Organization).WithMany(o => o.Departments).HasForeignKey(d => d.OrganizationId);
    }
}
