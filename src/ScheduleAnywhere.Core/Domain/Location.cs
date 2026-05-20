namespace ScheduleAnywhere.Core.Domain;

public class Location
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Organization? Organization { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
