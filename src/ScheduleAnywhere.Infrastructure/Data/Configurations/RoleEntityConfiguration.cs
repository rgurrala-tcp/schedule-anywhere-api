using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> b)
    {
        b.ToTable("t_role");
        b.HasKey(r => r.Id);
        b.Property(r => r.Id).HasColumnName("id_i");
        b.Property(r => r.Name).HasColumnName("name_vc").HasMaxLength(100);
        b.Property(r => r.Description).HasColumnName("description_vc");
    }
}
