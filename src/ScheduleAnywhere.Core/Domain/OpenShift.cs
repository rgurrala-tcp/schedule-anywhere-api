namespace ScheduleAnywhere.Core.Domain;

public class OpenShift
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int ShiftId { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Schedule? Schedule { get; set; }
    public Shift? Shift { get; set; }
    public ICollection<OpenShiftRequest> Requests { get; set; } = new List<OpenShiftRequest>();
}

public class OpenShiftRequest
{
    public int Id { get; set; }
    public int OpenShiftId { get; set; }
    public int EmployeeId { get; set; }
    public bool? IsApproved { get; set; }
    public string? Notes { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }

    public OpenShift? OpenShift { get; set; }
    public Employee? Employee { get; set; }
}
