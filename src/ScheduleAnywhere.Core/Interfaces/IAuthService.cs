namespace ScheduleAnywhere.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password, CancellationToken ct = default);
    Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
    Task LogoutAsync(int employeeId, CancellationToken ct = default);
    Task<bool> ValidateUsernameAsync(string username, int organizationId, CancellationToken ct = default);
}

public record AuthResult(string AccessToken, string RefreshToken, DateTime ExpiresAt, int EmployeeId, int OrganizationId, IReadOnlyList<string> Roles);
