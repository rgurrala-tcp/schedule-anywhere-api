using ScheduleAnywhere.Core.Interfaces;
using System.Security.Claims;

namespace ScheduleAnywhere.Api.Services;

public class CurrentUserService(IHttpContextAccessor accessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => accessor.HttpContext?.User;

    public int EmployeeId => int.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
    public int OrganizationId => int.TryParse(User?.FindFirstValue("org_id"), out var id) ? id : 0;
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
    public bool HasRole(string role) => User?.IsInRole(role) ?? false;
}
