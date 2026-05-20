using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IScheduleService
{
    Task<PagedResult<Schedule>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Schedule> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Schedule> CreateAsync(Schedule schedule, CancellationToken ct = default);
    Task<Schedule> UpdateAsync(Schedule schedule, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
    Task PostAsync(int id, int organizationId, CancellationToken ct = default);
}
