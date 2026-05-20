namespace ScheduleAnywhere.Core.Domain;

public class Schedule
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public bool IsPosted { get; set; }
    public DateTime? PostedDate { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Organization? Organization { get; set; }
    public ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    public ICollection<Explanation> Explanations { get; set; } = new List<Explanation>();
    public ICollection<EmployeeScheduleAccess> EmployeeAccesses { get; set; } = new List<EmployeeScheduleAccess>();
    public ICollection<Filter> Filters { get; set; } = new List<Filter>();
    public ICollection<Workgroup> Workgroups { get; set; } = new List<Workgroup>();
    public ICollection<CoverageWatch> CoverageWatches { get; set; } = new List<CoverageWatch>();
    public ICollection<OpenShift> OpenShifts { get; set; } = new List<OpenShift>();
}
