using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Organization;

[ApiController]
[Route("api/organizations/{organizationId:int}/positions")]
[Authorize]
public class PositionsController(IPositionService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<PositionResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<PositionResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<PositionResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageOrganizationPositions")]
    [ProducesResponseType<PositionResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreatePositionRequest req, CancellationToken ct)
    {
        var pos = new Position { OrganizationId = organizationId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(pos, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationPositions")]
    [ProducesResponseType<PositionResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdatePositionRequest req, CancellationToken ct)
    {
        var pos = await service.GetByIdAsync(id, organizationId, ct);
        pos.Name = req.Name; pos.Description = req.Description; pos.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(pos, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationPositions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static PositionResponse Map(Position p) => new(p.Id, p.OrganizationId, p.Name, p.Description, p.Active, p.LastModifiedDateTime.ToString("O"));
}
