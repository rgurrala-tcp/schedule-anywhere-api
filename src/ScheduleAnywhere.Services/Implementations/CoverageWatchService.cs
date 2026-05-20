using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class CoverageWatchService(IRepository<CoverageWatch> repo) : ICoverageWatchService
{
    public Task<PagedResult<CoverageWatch>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, c => c.ScheduleId == scheduleId, ct);

    public async Task<CoverageWatch> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var cw = await repo.GetByIdAsync(id, ct);
        if (cw is null || cw.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(CoverageWatch), id);
        return cw;
    }

    public async Task<CoverageWatch> CreateAsync(CoverageWatch coverageWatch, CancellationToken ct)
    {
        coverageWatch.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(coverageWatch, ct);
    }

    public async Task<CoverageWatch> UpdateAsync(CoverageWatch coverageWatch, CancellationToken ct)
    {
        coverageWatch.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(coverageWatch, ct);
        return coverageWatch;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var cw = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(cw, ct);
    }
}
