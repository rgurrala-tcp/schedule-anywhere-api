using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface ISkillService
{
    Task<PagedResult<Skill>> GetPagedAsync(int organizationId, int page, int pageSize, bool? active = null, CancellationToken ct = default);
    Task<Skill> GetByIdAsync(int id, int organizationId, CancellationToken ct = default);
    Task<Skill> CreateAsync(Skill skill, CancellationToken ct = default);
    Task<Skill> UpdateAsync(Skill skill, CancellationToken ct = default);
    Task DeleteAsync(int id, int organizationId, CancellationToken ct = default);
}
