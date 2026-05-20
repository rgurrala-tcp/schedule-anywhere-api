using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> b)
    {
        b.ToTable("t_employee");
        b.HasKey(e => e.Id);
        b.Property(e => e.Id).HasColumnName("id_i");
        b.Property(e => e.OrganizationId).HasColumnName("organization_id_i");
        b.Property(e => e.DepartmentId).HasColumnName("department_id_i");
        b.Property(e => e.LocationId).HasColumnName("location_id_i");
        b.Property(e => e.PositionId).HasColumnName("position_id_i");
        b.Property(e => e.SupervisorEmployeeId).HasColumnName("supervisor_employee_id_i");
        b.Property(e => e.FirstName).HasColumnName("firstname_nvc").HasMaxLength(50);
        b.Property(e => e.LastName).HasColumnName("lastname_nvc").HasMaxLength(50);
        b.Property(e => e.Username).HasColumnName("username_nvc").HasMaxLength(50);
        b.Property(e => e.PasswordHash).HasColumnName("password_nvc").HasMaxLength(256);
        b.Property(e => e.PasswordSalt).HasColumnName("password_salt_nvc").HasMaxLength(256);
        b.Property(e => e.Email1).HasColumnName("email1_nvc").HasMaxLength(100);
        b.Property(e => e.Email2).HasColumnName("email2_nvc").HasMaxLength(100);
        b.Property(e => e.Phone1).HasColumnName("phone1_nvc").HasMaxLength(30);
        b.Property(e => e.Mobile).HasColumnName("mobile_nvc").HasMaxLength(30);
        b.Property(e => e.Ssn).HasColumnName("ssn_nvc").HasMaxLength(20);
        b.Property(e => e.ExportIdentifier).HasColumnName("exportidentifier_nvc").HasMaxLength(100);
        b.Property(e => e.Comments).HasColumnName("comments_nvc");
        b.Property(e => e.LoginToken).HasColumnName("logintoken_nvc").HasMaxLength(100);
        b.Property(e => e.ResetPasswordToken).HasColumnName("resetpasswordtoken_nvc").HasMaxLength(100);
        b.Property(e => e.Active).HasColumnName("active_b");
        b.Property(e => e.CanLogin).HasColumnName("canlogin_b");
        b.Property(e => e.Created).HasColumnName("created_dt");
        b.Property(e => e.HireDate).HasColumnName("hiredate_dt");
        b.Property(e => e.Deactivated).HasColumnName("deactivated_dt");
        b.Property(e => e.LastLogin).HasColumnName("lastlogin_dt");
        b.Property(e => e.LastModifiedDateTime).HasColumnName("lastmodifieddatetime_dt");
        b.Property(e => e.Cost).HasColumnName("cost_m").HasPrecision(18, 4);
        b.Property(e => e.CostType).HasColumnName("costtype_i");

        b.HasIndex(e => new { e.OrganizationId, e.Username }).IsUnique();

        b.HasOne(e => e.Organization).WithMany(o => o.Employees).HasForeignKey(e => e.OrganizationId);
        b.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);
        b.HasOne(e => e.Location).WithMany(l => l.Employees).HasForeignKey(e => e.LocationId);
        b.HasOne(e => e.Position).WithMany(p => p.Employees).HasForeignKey(e => e.PositionId);
        b.HasOne(e => e.Supervisor).WithMany(e => e.DirectReports).HasForeignKey(e => e.SupervisorEmployeeId);

        b.Ignore(e => e.FullName);
    }
}

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> b)
    {
        b.ToTable("t_mapRoleEmployee");
        b.HasKey(e => new { e.EmployeeId, e.RoleId });
        b.Property(e => e.EmployeeId).HasColumnName("employee_id_i");
        b.Property(e => e.RoleId).HasColumnName("role_id_i");
        b.HasOne(e => e.Employee).WithMany(e => e.EmployeeRoles).HasForeignKey(e => e.EmployeeId);
        b.HasOne(e => e.RoleEntity).WithMany(r => r.EmployeeRoles).HasForeignKey(e => e.RoleId);
    }
}

public class EmployeeScheduleAccessConfiguration : IEntityTypeConfiguration<EmployeeScheduleAccess>
{
    public void Configure(EntityTypeBuilder<EmployeeScheduleAccess> b)
    {
        b.ToTable("t_mapScheduleEmployeeAccess");
        b.HasKey(e => new { e.EmployeeId, e.ScheduleId });
        b.Property(e => e.EmployeeId).HasColumnName("employee_id_i");
        b.Property(e => e.ScheduleId).HasColumnName("schedule_id_i");
        b.HasOne(e => e.Employee).WithMany(e => e.ScheduleAccesses).HasForeignKey(e => e.EmployeeId);
        b.HasOne(e => e.Schedule).WithMany(s => s.EmployeeAccesses).HasForeignKey(e => e.ScheduleId);
    }
}
