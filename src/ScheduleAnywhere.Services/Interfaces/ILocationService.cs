using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface ILocationService
{
    Task<PagedResult<Location>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Location> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Location> CreateAsync(Location location, CancellationToken ct = default);
    Task<Location> UpdateAsync(Location location, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
}
