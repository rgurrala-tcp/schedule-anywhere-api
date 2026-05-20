using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Organization;

[ApiController]
[Route("api/organizations/{organizationId:int}/skills")]
[Authorize]
public class SkillsController(ISkillService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<SkillResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<SkillResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<SkillResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType<SkillResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreateSkillRequest req, CancellationToken ct)
    {
        var skill = new Skill { OrganizationId = organizationId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(skill, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType<SkillResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdateSkillRequest req, CancellationToken ct)
    {
        var skill = await service.GetByIdAsync(id, organizationId, ct);
        skill.Name = req.Name; skill.Description = req.Description; skill.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(skill, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static SkillResponse Map(Skill s) => new(s.Id, s.OrganizationId, s.Name, s.Description, s.Active, s.LastModifiedDateTime.ToString("O"));
}
