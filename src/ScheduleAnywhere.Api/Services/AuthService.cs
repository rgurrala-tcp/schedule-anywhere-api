using Microsoft.IdentityModel.Tokens;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ScheduleAnywhere.Api.Services;

public class AuthService(
    IRepository<Employee> employeeRepo,
    IRepository<EmployeeRole> employeeRoleRepo,
    IRepository<RoleEntity> roleRepo,
    IConfiguration config) : IAuthService
{
    private static readonly TimeSpan AccessTokenLifetime = TimeSpan.FromHours(1);

    public async Task<AuthResult> LoginAsync(string username, string password, CancellationToken ct = default)
    {
        var employees = await employeeRepo.GetAllAsync(
            e => e.Username == username && e.PasswordHash == password && e.Active && e.CanLogin, ct);

        var employee = employees.FirstOrDefault()
            ?? throw new InvalidCredentialsException();

        //if (!VerifyPassword(password, employee.PasswordHash, employee.PasswordSalt))
        //    throw new InvalidCredentialsException();

        var roles = await GetRolesAsync(employee.Id, ct);
        var refreshToken = GenerateRefreshToken();

        employee.LoginToken = refreshToken;
        employee.LastLogin = DateTime.UtcNow;
        await employeeRepo.UpdateAsync(employee, ct);

        var (accessToken, expiresAt) = BuildAccessToken(employee, roles);
        return new AuthResult(accessToken, refreshToken, expiresAt, employee.Id, employee.OrganizationId, roles);
    }

    public async Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default)
    {
        var employees = await employeeRepo.GetAllAsync(
            e => e.LoginToken == refreshToken && e.Active, ct);

        var employee = employees.FirstOrDefault()
            ?? throw new InvalidCredentialsException();

        var roles = await GetRolesAsync(employee.Id, ct);
        var newRefreshToken = GenerateRefreshToken();

        employee.LoginToken = newRefreshToken;
        await employeeRepo.UpdateAsync(employee, ct);

        var (accessToken, expiresAt) = BuildAccessToken(employee, roles);
        return new AuthResult(accessToken, newRefreshToken, expiresAt, employee.Id, employee.OrganizationId, roles);
    }

    public async Task LogoutAsync(int employeeId, CancellationToken ct = default)
    {
        var employee = await employeeRepo.GetByIdAsync(employeeId, ct);
        if (employee is null) return;

        employee.LoginToken = null;
        await employeeRepo.UpdateAsync(employee, ct);
    }

    public Task<bool> ValidateUsernameAsync(string username, int organizationId, CancellationToken ct = default) =>
        employeeRepo.ExistsAsync(
            e => e.Username == username && e.OrganizationId == organizationId && e.Active && e.CanLogin, ct);

    // ── Private helpers ───────────────────────────────────────────────────────

    private async Task<IReadOnlyList<string>> GetRolesAsync(int employeeId, CancellationToken ct)
    {
        var employeeRoles = await employeeRoleRepo.GetAllAsync(er => er.EmployeeId == employeeId, ct);
        if (employeeRoles.Count == 0) return [];

        var roleIds = employeeRoles.Select(er => er.RoleId).ToHashSet();
        var roleEntities = await roleRepo.GetAllAsync(r => roleIds.Contains(r.Id), ct);
        return roleEntities.Select(r => r.Name).ToList();
    }

    private (string Token, DateTime ExpiresAt) BuildAccessToken(Employee employee, IReadOnlyList<string> roles)
    {
        var key = config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
        var issuer = config["Jwt:Issuer"] ?? "ScheduleAnywhere";
        var audience = config["Jwt:Audience"] ?? "ScheduleAnywhere";
        var expiresAt = DateTime.UtcNow.Add(AccessTokenLifetime);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("organizationId", employee.OrganizationId.ToString()),
            new("username", employee.Username),
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }

    private static string GenerateRefreshToken() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    private static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var computedHash = Convert.ToBase64String(
            Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltBytes,
                iterations: 100_000,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: 32));
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(computedHash),
            Encoding.UTF8.GetBytes(storedHash));
    }
}
