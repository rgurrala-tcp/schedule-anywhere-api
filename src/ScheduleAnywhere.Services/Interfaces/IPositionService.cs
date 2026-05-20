using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IPositionService
{
    Task<PagedResult<Position>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Position> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Position> CreateAsync(Position position, CancellationToken ct = default);
    Task<Position> UpdateAsync(Position position, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
}
