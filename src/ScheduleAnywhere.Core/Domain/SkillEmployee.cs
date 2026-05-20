namespace ScheduleAnywhere.Core.Domain;

public class SkillEmployee
{
    public int SkillId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Skill? Skill { get; set; }
    public Employee? Employee { get; set; }
}
