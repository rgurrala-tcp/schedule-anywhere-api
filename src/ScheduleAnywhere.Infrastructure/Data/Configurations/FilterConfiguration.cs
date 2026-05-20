using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class FilterConfiguration : IEntityTypeConfiguration<Filter>
{
    public void Configure(EntityTypeBuilder<Filter> b)
    {
        b.ToTable("t_filter");
        b.HasKey(f => f.Id);
        b.Property(f => f.Id).HasColumnName("id_i");
        b.Property(f => f.ScheduleId).HasColumnName("schedule_id_i");
        b.Property(f => f.Name).HasColumnName("name_nvc").HasMaxLength(100);
        b.Property(f => f.Description).HasColumnName("description_nvc");
        b.Property(f => f.Active).HasColumnName("active_b");
        b.Property(f => f.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.HasOne(f => f.Schedule).WithMany(s => s.Filters).HasForeignKey(f => f.ScheduleId);
    }
}

public class FilterDepartmentConfiguration : IEntityTypeConfiguration<FilterDepartment>
{
    public void Configure(EntityTypeBuilder<FilterDepartment> b)
    {
        b.ToTable("t_mapFilterDepartment");
        b.HasKey(f => new { f.FilterId, f.DepartmentId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.DepartmentId).HasColumnName("department_id_i");
    }
}

public class FilterLocationConfiguration : IEntityTypeConfiguration<FilterLocation>
{
    public void Configure(EntityTypeBuilder<FilterLocation> b)
    {
        b.ToTable("t_mapFilterLocation");
        b.HasKey(f => new { f.FilterId, f.LocationId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.LocationId).HasColumnName("location_id_i");
    }
}

public class FilterPositionConfiguration : IEntityTypeConfiguration<FilterPosition>
{
    public void Configure(EntityTypeBuilder<FilterPosition> b)
    {
        b.ToTable("t_mapFilterPosition");
        b.HasKey(f => new { f.FilterId, f.PositionId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.PositionId).HasColumnName("position_id_i");
    }
}

public class FilterEmployeeConfiguration : IEntityTypeConfiguration<FilterEmployee>
{
    public void Configure(EntityTypeBuilder<FilterEmployee> b)
    {
        b.ToTable("t_mapFilterEmployee");
        b.HasKey(f => new { f.FilterId, f.EmployeeId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.EmployeeId).HasColumnName("employee_id_i");
    }
}

public class FilterShiftConfiguration : IEntityTypeConfiguration<FilterShift>
{
    public void Configure(EntityTypeBuilder<FilterShift> b)
    {
        b.ToTable("t_mapFilterShift");
        b.HasKey(f => new { f.FilterId, f.ShiftId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.ShiftId).HasColumnName("shift_id_i");
    }
}

public class FilterExplanationConfiguration : IEntityTypeConfiguration<FilterExplanation>
{
    public void Configure(EntityTypeBuilder<FilterExplanation> b)
    {
        b.ToTable("t_mapFilterExplanation");
        b.HasKey(f => new { f.FilterId, f.ExplanationId });
        b.Property(f => f.FilterId).HasColumnName("filter_id_i");
        b.Property(f => f.ExplanationId).HasColumnName("explanation_id_i");
    }
}
