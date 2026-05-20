using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class ScheduleItemService(IRepository<ScheduleItem> repo) : IScheduleItemService
{
    public Task<PagedResult<ScheduleItem>> GetPagedAsync(int scheduleId, int page, int pageSize, DateTime? from, DateTime? to, int? employeeId, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, i =>
            i.ScheduleId == scheduleId &&
            (from == null || i.Date >= from) &&
            (to == null || i.Date <= to) &&
            (employeeId == null || i.EmployeeId == employeeId), ct);

    public async Task<ScheduleItem> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var item = await repo.GetByIdAsync(id, ct);
        if (item is null || item.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(ScheduleItem), id);
        return item;
    }

    public async Task<ScheduleItem> CreateAsync(ScheduleItem item, CancellationToken ct)
    {
        item.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(item, ct);
    }

    public async Task<ScheduleItem> UpdateAsync(ScheduleItem item, CancellationToken ct)
    {
        item.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(item, ct);
        return item;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var item = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(item, ct);
    }
}
