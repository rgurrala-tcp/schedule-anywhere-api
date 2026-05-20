using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IShiftService
{
    Task<PagedResult<Shift>> GetPagedAsync(int scheduleId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Shift> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<Shift> CreateAsync(Shift shift, CancellationToken ct = default);
    Task<Shift> UpdateAsync(Shift shift, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
