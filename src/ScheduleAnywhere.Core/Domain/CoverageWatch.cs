namespace ScheduleAnywhere.Core.Domain;

public class CoverageWatch
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public ICollection<RequirementRow> RequirementRows { get; set; } = new List<RequirementRow>();
    public ICollection<CoverageWatchDepartment> Departments { get; set; } = new List<CoverageWatchDepartment>();
    public ICollection<CoverageWatchPosition> Positions { get; set; } = new List<CoverageWatchPosition>();
    public ICollection<CoverageWatchShift> Shifts { get; set; } = new List<CoverageWatchShift>();
}

public class CoverageWatchDepartment { public int CoverageWatchId { get; set; } public int DepartmentId { get; set; } public CoverageWatch? CoverageWatch { get; set; } public Department? Department { get; set; } }
public class CoverageWatchPosition   { public int CoverageWatchId { get; set; } public int PositionId { get; set; }   public CoverageWatch? CoverageWatch { get; set; } public Position? Position { get; set; } }
public class CoverageWatchShift      { public int CoverageWatchId { get; set; } public int ShiftId { get; set; }      public CoverageWatch? CoverageWatch { get; set; } public Shift? Shift { get; set; } }
