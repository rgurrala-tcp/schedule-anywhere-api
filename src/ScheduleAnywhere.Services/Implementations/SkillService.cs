using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class SkillService(IRepository<Skill> repo) : ISkillService
{
    public Task<PagedResult<Skill>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active, CancellationToken ct) =>
        repo.GetPagedAsync(page, pageSize, s => s.OrganizationId == organizationId && (active == null || s.Active == active), ct);

    public async Task<Skill> GetByIdAsync(int id, int organizationId, CancellationToken ct)
    {
        var skill = await repo.GetByIdAsync(id, ct);
        if (skill is null || skill.OrganizationId != organizationId)
            throw new NotFoundException(nameof(Skill), id);
        return skill;
    }

    public async Task<Skill> CreateAsync(Skill skill, CancellationToken ct)
    {
        skill.LastModifiedDateTime = DateTime.UtcNow;
        return await repo.AddAsync(skill, ct);
    }

    public async Task<Skill> UpdateAsync(Skill skill, CancellationToken ct)
    {
        skill.LastModifiedDateTime = DateTime.UtcNow;
        await repo.UpdateAsync(skill, ct);
        return skill;
    }

    public async Task DeleteAsync(int id, int organizationId, CancellationToken ct)
    {
        var skill = await GetByIdAsync(id, organizationId, ct);
        await repo.DeleteAsync(skill, ct);
    }
}
