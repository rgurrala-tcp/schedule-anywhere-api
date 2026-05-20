using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/filters")]
[Authorize]
public class FiltersController(IFilterService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<FilterResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] int page = 1, [FromQuery] int pageSize = 25, CancellationToken ct = default)
    {
        var result = await service.GetPagedAsync(scheduleId, page, pageSize, ct);
        return Ok(new PagedResponse<FilterResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<FilterResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<FilterResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateFilterRequest req, CancellationToken ct)
    {
        var filter = new Filter { ScheduleId = scheduleId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(filter, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<FilterResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateFilterRequest req, CancellationToken ct)
    {
        var filter = await service.GetByIdAsync(id, scheduleId, ct);
        filter.Name = req.Name; filter.Description = req.Description; filter.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(filter, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static FilterResponse Map(Filter f) => new(f.Id, f.ScheduleId, f.Name, f.Description, f.Active, f.LastModifiedDateTime.ToString("O"));
}
