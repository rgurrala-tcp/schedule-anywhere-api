using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IFilterService
{
    Task<PagedResult<Filter>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct = default);
    Task<Filter> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<Filter> CreateAsync(Filter filter, CancellationToken ct = default);
    Task<Filter> UpdateAsync(Filter filter, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
