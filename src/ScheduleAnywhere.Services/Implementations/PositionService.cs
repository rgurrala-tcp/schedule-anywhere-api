using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class PositionService(IRepository<Position> repo) : IPositionService
{
    public Task<PagedResult<Position>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, p => p.OrganizationId == organizationId && (active == null || p.Active == active), ct);

    public async Task<Position> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var pos = await repo.GetByIdAsync(id, ct);
        if (pos is null || pos.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Position), id);
        return pos;
    }

    public async Task<Position> CreateAsync(Position position, CancellationToken ct)
    {
        position.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(position, ct);
    }

    public async Task<Position> UpdateAsync(Position position, CancellationToken ct)
    {
        position.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(position, ct);
        return position;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var pos = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(pos, ct);
    }
}
