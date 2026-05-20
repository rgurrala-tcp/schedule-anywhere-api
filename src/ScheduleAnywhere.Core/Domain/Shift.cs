namespace ScheduleAnywhere.Core.Domain;

public class Shift
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int ColorId { get; set; }
    public int? ParentShiftId { get; set; }
    public int? ShiftTagId { get; set; }
    public int? TimeOffTagId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Code { get; set; }

    public bool Active { get; set; }
    public bool IsTimeOff { get; set; }
    public ShiftViewType ShiftView { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime StopTime { get; set; }
    public int BreakMinutes { get; set; }
    public decimal? PaidHours { get; set; }
    public decimal? UnpaidHours { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public ShiftColor? Color { get; set; }
    public Shift? ParentShift { get; set; }
    public ICollection<Shift> ChildShifts { get; set; } = new List<Shift>();
}
