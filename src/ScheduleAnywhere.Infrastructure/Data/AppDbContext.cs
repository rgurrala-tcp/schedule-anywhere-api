using Microsoft.EntityFrameworkCore;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();
    public DbSet<EmployeeScheduleAccess> EmployeeScheduleAccesses => Set<EmployeeScheduleAccess>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<ScheduleItem> ScheduleItems => Set<ScheduleItem>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<ShiftColor> ShiftColors => Set<ShiftColor>();
    public DbSet<Explanation> Explanations => Set<Explanation>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<SkillEmployee> SkillEmployees => Set<SkillEmployee>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<Workgroup> Workgroups => Set<Workgroup>();
    public DbSet<CoverageWatch> CoverageWatches => Set<CoverageWatch>();
    public DbSet<RequirementRow> RequirementRows => Set<RequirementRow>();
    public DbSet<OpenShift> OpenShifts => Set<OpenShift>();
    public DbSet<OpenShiftRequest> OpenShiftRequests => Set<OpenShiftRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
