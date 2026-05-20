using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class ShiftService(IRepository<Shift> repo) : IShiftService
{
    public Task<PagedResult<Shift>> GetPagedAsync(int scheduleId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, s => s.ScheduleId == scheduleId && (active == null || s.Active == active), ct);

    public async Task<Shift> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var shift = await repo.GetByIdAsync(id, ct);
        if (shift is null || shift.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(Shift), id);
        return shift;
    }

    public async Task<Shift> CreateAsync(Shift shift, CancellationToken ct)
    {
        var duplicate = await repo.ExistsAsync(s => s.ScheduleId == shift.ScheduleId && s.Name == shift.Name, ct);
        if (duplicate) throw new DuplicateNameException(nameof(Shift), shift.Name);
        shift.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(shift, ct);
    }

    public async Task<Shift> UpdateAsync(Shift shift, CancellationToken ct)
    {
        shift.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(shift, ct);
        return shift;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var shift = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(shift, ct);
    }
}
