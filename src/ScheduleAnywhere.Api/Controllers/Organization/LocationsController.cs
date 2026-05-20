using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Organization;

[ApiController]
[Route("api/organizations/{organizationId:int}/locations")]
[Authorize]
public class LocationsController(ILocationService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<LocationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<LocationResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<LocationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageOrganizationLocations")]
    [ProducesResponseType<LocationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreateLocationRequest req, CancellationToken ct)
    {
        var loc = new Location { OrganizationId = organizationId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(loc, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationLocations")]
    [ProducesResponseType<LocationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdateLocationRequest req, CancellationToken ct)
    {
        var loc = await service.GetByIdAsync(id, organizationId, ct);
        loc.Name = req.Name; loc.Description = req.Description; loc.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(loc, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationLocations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static LocationResponse Map(Location l) => new(l.Id, l.OrganizationId, l.Name, l.Description, l.Active, l.LastModifiedDateTime.ToString("O"));
}
