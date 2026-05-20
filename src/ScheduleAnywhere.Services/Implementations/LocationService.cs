using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class LocationService(IRepository<Location> repo) : ILocationService
{
    public Task<PagedResult<Location>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, l => l.OrganizationId == organizationId && (active == null || l.Active == active), ct);

    public async Task<Location> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var loc = await repo.GetByIdAsync(id, ct);
        if (loc is null || loc.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Location), id);
        return loc;
    }

    public async Task<Location> CreateAsync(Location location, CancellationToken ct)
    {
        location.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(location, ct);
    }

    public async Task<Location> UpdateAsync(Location location, CancellationToken ct)
    {
        location.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(location, ct);
        return location;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var loc = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(loc, ct);
    }
}
