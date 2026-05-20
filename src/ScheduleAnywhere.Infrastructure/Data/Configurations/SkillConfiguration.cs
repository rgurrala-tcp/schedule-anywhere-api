using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> b)
    {
        b.ToTable("t_skill");
        b.HasKey(s => s.Id);
        b.Property(s => s.Id).HasColumnName("id_i");
        b.Property(s => s.OrganizationId).HasColumnName("organization_id_i");
        b.Property(s => s.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(s => s.Description).HasColumnName("description_nvc");
        b.Property(s => s.Active).HasColumnName("active_b");
        b.Property(s => s.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
    }
}

public class SkillEmployeeConfiguration : IEntityTypeConfiguration<SkillEmployee>
{
    public void Configure(EntityTypeBuilder<SkillEmployee> b)
    {
        b.ToTable("t_mapSkillEmployee");
        b.HasKey(s => new { s.SkillId, s.EmployeeId });
        b.Property(s => s.SkillId).HasColumnName("skill_id_i");
        b.Property(s => s.EmployeeId).HasColumnName("employee_id_i");
        b.Property(s => s.ExpirationDate).HasColumnName("expirationdate_dt");
        b.HasOne(s => s.Skill).WithMany(s => s.SkillEmployees).HasForeignKey(s => s.SkillId);
        b.HasOne(s => s.Employee).WithMany(e => e.Skills).HasForeignKey(s => s.EmployeeId);
    }
}
