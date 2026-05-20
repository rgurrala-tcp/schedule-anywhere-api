using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class DepartmentService(IRepository<Department> repo) : IDepartmentService
{
    public Task<PagedResult<Department>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, d => d.OrganizationId == organizationId && (active == null || d.Active == active), ct);

    public async Task<Department> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var dept = await repo.GetByIdAsync(id, ct);
        if (dept is null || dept.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Department), id);
        return dept;
    }

    public async Task<Department> CreateAsync(Department department, CancellationToken ct)
    {
        department.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(department, ct);
    }

    public async Task<Department> UpdateAsync(Department department, CancellationToken ct)
    {
        department.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(department, ct);
        return department;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var dept = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(dept, ct);
    }
}
