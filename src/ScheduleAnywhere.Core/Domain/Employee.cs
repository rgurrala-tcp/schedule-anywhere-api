namespace ScheduleAnywhere.Core.Domain;

public class Employee
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int DepartmentId { get; set; }
    public int LocationId { get; set; }
    public int PositionId { get; set; }
    public int? SupervisorEmployeeId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string? Email1 { get; set; }
    public string? Email2 { get; set; }
    public string? Phone1 { get; set; }
    public string? Mobile { get; set; }
    public string? Ssn { get; set; }
    public string? ExportIdentifier { get; set; }
    public string? Comments { get; set; }
    public string? LoginToken { get; set; }
    public string? ResetPasswordToken { get; set; }

    public bool Active { get; set; }
    public bool CanLogin { get; set; }
    public DateTime Created { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? Deactivated { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public decimal? Cost { get; set; }
    public CostType? CostType { get; set; }

    public Organization? Organization { get; set; }
    public Department? Department { get; set; }
    public Location? Location { get; set; }
    public Position? Position { get; set; }
    public Employee? Supervisor { get; set; }
    public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();
    public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    public ICollection<EmployeeScheduleAccess> ScheduleAccesses { get; set; } = new List<EmployeeScheduleAccess>();
    public ICollection<SkillEmployee> Skills { get; set; } = new List<SkillEmployee>();

    public string FullName => $"{FirstName} {LastName}";
}

public class EmployeeRole
{
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }
    public Employee? Employee { get; set; }
    public RoleEntity? RoleEntity { get; set; }
}

public class EmployeeScheduleAccess
{
    public int EmployeeId { get; set; }
    public int ScheduleId { get; set; }
    public Employee? Employee { get; set; }
    public Schedule? Schedule { get; set; }
}
