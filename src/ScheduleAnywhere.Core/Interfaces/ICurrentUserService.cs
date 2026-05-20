namespace ScheduleAnywhere.Core.Interfaces;

public interface ICurrentUserService
{
    int EmployeeId { get; }
    int OrganizationId { get; }
    bool IsAuthenticated { get; }
    bool HasRole(string role);
}
