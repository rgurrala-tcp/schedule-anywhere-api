namespace ScheduleAnywhere.Core.Domain;

public class RequirementRow
{
    public int Id { get; set; }
    public int CoverageWatchId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int MinimumCount { get; set; }
    public CoverageWatch? CoverageWatch { get; set; }
}
