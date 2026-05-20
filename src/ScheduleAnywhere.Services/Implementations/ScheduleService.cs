using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class ScheduleService(IRepository<Schedule> repo) : IScheduleService
{
    public Task<PagedResult<Schedule>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, s => s.OrganizationId == organizationId && (active == null || s.Active == active), ct);

    public async Task<Schedule> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var schedule = await repo.GetByIdAsync(id, ct);
        if (schedule is null || schedule.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Schedule), id);
        return schedule;
    }

    public async Task<Schedule> CreateAsync(Schedule schedule, CancellationToken ct)
    {
        schedule.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(schedule, ct);
    }

    public async Task<Schedule> UpdateAsync(Schedule schedule, CancellationToken ct)
    {
        schedule.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(schedule, ct);
        return schedule;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var schedule = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(schedule, ct);
    }

    public async Task PostAsync(int id, int organizationId, CancellationToken ct)
    {
        var schedule = await GetByIdAsync(id, organizationId, ct);
        schedule.IsPosted = true;
        schedule.PostedDate = DateTime.UtcNow;
        await repo.UpdateAsync(schedule, ct);
    }
}
