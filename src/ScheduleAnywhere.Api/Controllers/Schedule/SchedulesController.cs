using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/organizations/{organizationId:int}/schedules")]
[Authorize]
public class SchedulesController(IScheduleService service) : ControllerBase
{
    /// <summary>Returns a paged list of schedules for an organization.</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<ScheduleResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<ScheduleResponse>(
            result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    /// <summary>Returns a single schedule by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ScheduleResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    /// <summary>Creates a new schedule.</summary>
    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ScheduleResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreateScheduleRequest request, CancellationToken ct)
    {
        var schedule = new Core.Domain.Schedule { OrganizationId = organizationId, Name = request.Name, Description = request.Description, Active = request.Active };
        var created = await service.CreateAsync(schedule, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    /// <summary>Updates an existing schedule.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ScheduleResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdateScheduleRequest request, CancellationToken ct)
    {
        var schedule = await service.GetByIdAsync(id, organizationId, ct);
        schedule.Name = request.Name;
        schedule.Description = request.Description;
        schedule.Active = request.Active;
        return Ok(Map(await service.UpdateAsync(schedule, ct)));
    }

    /// <summary>Publishes a schedule so employees with VIEW_PERSONAL_SCHEDULE can see it.</summary>
    [HttpPost("{id:int}/post")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post(int organizationId, int id, CancellationToken ct)
    {
        await service.PostAsync(id, organizationId, ct);
        return NoContent();
    }

    /// <summary>Deletes a schedule. Cannot be deleted while schedule items exist.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static ScheduleResponse Map(Core.Domain.Schedule s) => new(
        s.Id, s.OrganizationId, s.Name, s.Description,
        s.Active, s.IsPosted, s.PostedDate?.ToString("O"), s.LastModifiedDateTime.ToString("O"));
}
