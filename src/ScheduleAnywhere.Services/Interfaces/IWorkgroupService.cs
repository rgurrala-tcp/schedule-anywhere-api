using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IWorkgroupService
{
    Task<PagedResult<Workgroup>> GetPagedAsync(int scheduleId, int page, int pageSize, CancellationToken ct = default);
    Task<Workgroup> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<Workgroup> CreateAsync(Workgroup workgroup, CancellationToken ct = default);
    Task<Workgroup> UpdateAsync(Workgroup workgroup, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
}
