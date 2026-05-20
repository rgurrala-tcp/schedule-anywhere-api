using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class WorkgroupService(IRepository<Workgroup> repo) : IWorkgroupService
{
    public Task<PagedResult<Workgroup>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, w => w.ScheduleId == scheduleId, ct);

    public async Task<Workgroup> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var wg = await repo.GetByIdAsync(id, ct);
        if (wg is null || wg.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(Workgroup), id);
        return wg;
    }

    public async Task<Workgroup> CreateAsync(Workgroup workgroup, CancellationToken ct)
    {
        workgroup.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(workgroup, ct);
    }

    public async Task<Workgroup> UpdateAsync(Workgroup workgroup, CancellationToken ct)
    {
        workgroup.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(workgroup, ct);
        return workgroup;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var wg = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(wg, ct);
    }
}
