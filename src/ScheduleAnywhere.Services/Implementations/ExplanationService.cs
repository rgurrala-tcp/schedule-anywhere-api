using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class ExplanationService(IRepository<Explanation> repo) : IExplanationService
{
    public Task<PagedResult<Explanation>> GetPagedAsync(int scheduleId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, e => e.ScheduleId == scheduleId && (active == null || e.Active == active), ct);

    public async Task<Explanation> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var exp = await repo.GetByIdAsync(id, ct);
        if (exp is null || exp.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(Explanation), id);
        return exp;
    }

    public async Task<Explanation> CreateAsync(Explanation explanation, CancellationToken ct)
    {
        var duplicate = await repo.ExistsAsync(e => e.ScheduleId == explanation.ScheduleId && e.Name == explanation.Name, ct);
        if (duplicate) throw new DuplicateNameException(nameof(Explanation), explanation.Name);
        explanation.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(explanation, ct);
    }

    public async Task<Explanation> UpdateAsync(Explanation explanation, CancellationToken ct)
    {
        explanation.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(explanation, ct);
        return explanation;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var exp = await GetByIdAsync(id, scheduleId, ct);
        await repo.DeleteAsync(exp, ct);
    }
}
