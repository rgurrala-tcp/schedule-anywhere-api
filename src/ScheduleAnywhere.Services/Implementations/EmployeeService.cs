using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class EmployeeService(IRepository<Employee> repo) : IEmployeeService
{
    public async Task<PagedResult<Employee>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, string? search, CancellationToken ct)
    {
        return await repo.GetPagedAsync(page, pageSize,
            e => e.OrganizationId == organizationId
              && (active == null || e.Active == active)
              && (search == null || e.FirstName.Contains(search) || e.LastName.Contains(search) || e.Username.Contains(search)),
            ct);
    }

    public async Task<Employee> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var employee = await repo.GetByIdAsync(id, ct);
        if (employee is null || employee.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Employee), id);
        return employee;
    }

    public async Task<Employee> CreateAsync(Employee employee, CancellationToken ct)
    {
        var duplicate = await repo.ExistsAsync(
            e => e.OrganizationId == employee.OrganizationId && e.Username == employee.Username, ct);
        if (duplicate) throw new DuplicateNameException(nameof(Employee), employee.Username);

        employee.Created = DateTime.UtcNow;
        employee.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(employee, ct);
    }

    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken ct)
    {
        employee.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(employee, ct);
        return employee;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var employee = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(employee, ct);
    }
}
