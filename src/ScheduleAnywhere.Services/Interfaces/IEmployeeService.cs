using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IEmployeeService
{
    Task<PagedResult<Employee>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, string? search = null, CancellationToken ct = default);
    Task<Employee> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Employee> CreateAsync(Employee employee, CancellationToken ct = default);
    Task<Employee> UpdateAsync(Employee employee, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
}
