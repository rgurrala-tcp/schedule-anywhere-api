namespace ScheduleAnywhere.Core.Domain;

public class Filter
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public ICollection<FilterDepartment> Departments { get; set; } = new List<FilterDepartment>();
    public ICollection<FilterLocation> Locations { get; set; } = new List<FilterLocation>();
    public ICollection<FilterPosition> Positions { get; set; } = new List<FilterPosition>();
    public ICollection<FilterEmployee> Employees { get; set; } = new List<FilterEmployee>();
    public ICollection<FilterShift> Shifts { get; set; } = new List<FilterShift>();
    public ICollection<FilterExplanation> Explanations { get; set; } = new List<FilterExplanation>();
}

public class FilterDepartment  { public int FilterId { get; set; } public int DepartmentId { get; set; } public Filter? Filter { get; set; } public Department? Department { get; set; } }
public class FilterLocation    { public int FilterId { get; set; } public int LocationId { get; set; }   public Filter? Filter { get; set; } public Location? Location { get; set; } }
public class FilterPosition    { public int FilterId { get; set; } public int PositionId { get; set; }   public Filter? Filter { get; set; } public Position? Position { get; set; } }
public class FilterEmployee    { public int FilterId { get; set; } public int EmployeeId { get; set; }   public Filter? Filter { get; set; } public Employee? Employee { get; set; } }
public class FilterShift       { public int FilterId { get; set; } public int ShiftId { get; set; }      public Filter? Filter { get; set; } public Shift? Shift { get; set; } }
public class FilterExplanation { public int FilterId { get; set; } public int ExplanationId { get; set; } public Filter? Filter { get; set; } public Explanation? Explanation { get; set; } }

public class Workgroup
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public ICollection<WorkgroupDepartment> Departments { get; set; } = new List<WorkgroupDepartment>();
    public ICollection<WorkgroupLocation> Locations { get; set; } = new List<WorkgroupLocation>();
    public ICollection<WorkgroupPosition> Positions { get; set; } = new List<WorkgroupPosition>();
    public ICollection<WorkgroupEmployee> Employees { get; set; } = new List<WorkgroupEmployee>();
    public ICollection<WorkgroupSkill> Skills { get; set; } = new List<WorkgroupSkill>();
}

public class WorkgroupDepartment { public int WorkgroupId { get; set; } public int DepartmentId { get; set; } public Workgroup? Workgroup { get; set; } public Department? Department { get; set; } }
public class WorkgroupLocation   { public int WorkgroupId { get; set; } public int LocationId { get; set; }   public Workgroup? Workgroup { get; set; } public Location? Location { get; set; } }
public class WorkgroupPosition   { public int WorkgroupId { get; set; } public int PositionId { get; set; }   public Workgroup? Workgroup { get; set; } public Position? Position { get; set; } }
public class WorkgroupEmployee   { public int WorkgroupId { get; set; } public int EmployeeId { get; set; }   public Workgroup? Workgroup { get; set; } public Employee? Employee { get; set; } }
public class WorkgroupSkill      { public int WorkgroupId { get; set; } public int SkillId { get; set; }      public Workgroup? Workgroup { get; set; } public Skill? Skill { get; set; } }
