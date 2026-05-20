using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IExplanationService
{
    Task<PagedResult<Explanation>> GetPagedAsync(int scheduleId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Explanation> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<Explanation> CreateAsync(Explanation explanation, CancellationToken ct = default);
    Task<Explanation> UpdateAsync(Explanation explanation, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
