using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class WorkgroupConfiguration : IEntityTypeConfiguration<Workgroup>
{
    public void Configure(EntityTypeBuilder<Workgroup> b)
    {
        b.ToTable("t_workgroup");
        b.HasKey(w => w.Id);
        b.Property(w => w.Id).HasColumnName("id_i");
        b.Property(w => w.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(w => w.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(w => w.Description).HasColumnName("description_nvc");
        b.Property(w => w.Active).HasColumnName("active_b");
        b.Property(w => w.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(w => w.Schedule).WithMany(s => s.Workgroups).HasForeignKey(w => w.ScheduleId);
    }
}

public class WorkgroupDepartmentConfiguration : IEntityTypeConfiguration<WorkgroupDepartment>
{
    public void Configure(EntityTypeBuilder<WorkgroupDepartment> b)
    {
        b.ToTable("t_mapWorkgroupDepartment");
        b.HasKey(w => new { w.WorkgroupId, w.DepartmentId });
        b.Property(w => w.WorkgroupId).HasColumnName("workgroup_id_i");
        b.Property(w => w.DepartmentId).HasColumnName("department_id_i");
    }
}

public class WorkgroupLocationConfiguration : IEntityTypeConfiguration<WorkgroupLocation>
{
    public void Configure(EntityTypeBuilder<WorkgroupLocation> b)
    {
        b.ToTable("t_mapWorkgroupLocation");
        b.HasKey(w => new { w.WorkgroupId, w.LocationId });
        b.Property(w => w.WorkgroupId).HasColumnName("workgroup_id_i");
        b.Property(w => w.LocationId).HasColumnName("location_id_i");
    }
}

public class WorkgroupPositionConfiguration : IEntityTypeConfiguration<WorkgroupPosition>
{
    public void Configure(EntityTypeBuilder<WorkgroupPosition> b)
    {
        b.ToTable("t_mapWorkgroupPosition");
        b.HasKey(w => new { w.WorkgroupId, w.PositionId });
        b.Property(w => w.WorkgroupId).HasColumnName("workgroup_id_i");
        b.Property(w => w.PositionId).HasColumnName("position_id_i");
    }
}

public class WorkgroupEmployeeConfiguration : IEntityTypeConfiguration<WorkgroupEmployee>
{
    public void Configure(EntityTypeBuilder<WorkgroupEmployee> b)
    {
        b.ToTable("t_mapWorkgroupEmployee");
        b.HasKey(w => new { w.WorkgroupId, w.EmployeeId });
        b.Property(w => w.WorkgroupId).HasColumnName("workgroup_id_i");
        b.Property(w => w.EmployeeId).HasColumnName("employee_id_i");
    }
}

public class WorkgroupSkillConfiguration : IEntityTypeConfiguration<WorkgroupSkill>
{
    public void Configure(EntityTypeBuilder<WorkgroupSkill> b)
    {
        b.ToTable("t_mapWorkgroupSkill");
        b.HasKey(w => new { w.WorkgroupId, w.SkillId });
        b.Property(w => w.WorkgroupId).HasColumnName("workgroup_id_i");
        b.Property(w => w.SkillId).HasColumnName("skill_id_i");
    }
}
