using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/coverage-watches")]
[Authorize]
public class CoverageWatchesController(ICoverageWatchService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<CoverageWatchResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] int page = 1, [FromQuery] int pageSize = 25, CancellationToken ct = default)
    {
        var result = await service.GetPagedAsync(scheduleId, page, pageSize, ct);
        return Ok(new PagedResponse<CoverageWatchResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<CoverageWatchResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<CoverageWatchResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateCoverageWatchRequest req, CancellationToken ct)
    {
        var cw = new CoverageWatch { ScheduleId = scheduleId, Name = req.Name, Active = req.Active };
        var created = await service.CreateAsync(cw, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<CoverageWatchResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateCoverageWatchRequest req, CancellationToken ct)
    {
        var cw = await service.GetByIdAsync(id, scheduleId, ct);
        cw.Name = req.Name; cw.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(cw, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static CoverageWatchResponse Map(CoverageWatch c) => new(c.Id, c.ScheduleId, c.Name, c.Active, c.LastModifiedDateTime.ToString("O"));
}
