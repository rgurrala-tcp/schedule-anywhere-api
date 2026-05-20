namespace ScheduleAnywhere.Core.Domain;

public class ScheduleItem
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int EmployeeId { get; set; }
    public int? ShiftId { get; set; }
    public int? ExplanationId { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    public decimal? PaidHours { get; set; }
    public decimal? UnpaidHours { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public Employee? Employee { get; set; }
    public Shift? Shift { get; set; }
    public Explanation? Explanation { get; set; }
}
