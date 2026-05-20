namespace ScheduleAnywhere.Core.Domain;

public class Organization
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public OrganizationStatus Status { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
