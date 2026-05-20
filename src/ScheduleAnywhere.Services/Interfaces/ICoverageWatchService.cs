using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface ICoverageWatchService
{
    Task<PagedResult<CoverageWatch>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct = default);
    Task<CoverageWatch> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<CoverageWatch> CreateAsync(CoverageWatch coverageWatch, CancellationToken ct = default);
    Task<CoverageWatch> UpdateAsync(CoverageWatch coverageWatch, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
