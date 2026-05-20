using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class FilterService(IRepository<Filter> repo) : IFilterService
{
    public Task<PagedResult<Filter>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, f => f.ScheduleId == scheduleId, ct);

    public async Task<Filter> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var filter = await repo.GetByIdAsync(id, ct);
        if (filter is null || filter.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(Filter), id);
        return filter;
    }

    public async Task<Filter> CreateAsync(Filter filter, CancellationToken ct)
    {
        filter.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(filter, ct);
    }

    public async Task<Filter> UpdateAsync(Filter filter, CancellationToken ct)
    {
        filter.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(filter, ct);
        return filter;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var filter = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(filter, ct);
    }
}
