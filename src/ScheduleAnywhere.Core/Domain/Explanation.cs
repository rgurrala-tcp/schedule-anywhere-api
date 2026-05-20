namespace ScheduleAnywhere.Core.Domain;

public class Explanation
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Abbreviation { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Active { get; set; }
    public bool IsTimeOff { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Schedule? Schedule { get; set; }
}
