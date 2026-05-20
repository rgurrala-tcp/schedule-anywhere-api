using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IScheduleItemService
{
    Task<PagedResult<ScheduleItem>> GetPagedAsync(int scheduleId, int page, int pageSize, DateTime? from = null, DateTime? to = null, int? employeeId = null, CancellationToken ct = default);
    Task<ScheduleItem> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<ScheduleItem> CreateAsync(ScheduleItem item, CancellationToken ct = default);
    Task<ScheduleItem> UpdateAsync(ScheduleItem item, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
