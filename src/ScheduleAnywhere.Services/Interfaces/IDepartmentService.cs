using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IDepartmentService
{
    Task<PagedResult<Department>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Department> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Department> CreateAsync(Department department, CancellationToken ct = default);
    Task<Department> UpdateAsync(Department department, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
}
