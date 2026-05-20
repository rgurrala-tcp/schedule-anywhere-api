using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/workgroups")]
[Authorize]
public class WorkgroupsController(IWorkgroupService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<WorkgroupResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] int page = 1, [FromQuery] int pageSize = 25, CancellationToken ct = default)
    {
        var result = await service.GetPagedAsync(scheduleId, page, pageSize, ct);
        return Ok(new PagedResponse<WorkgroupResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<WorkgroupResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<WorkgroupResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateWorkgroupRequest req, CancellationToken ct)
    {
        var wg = new Workgroup { ScheduleId = scheduleId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(wg, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<WorkgroupResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateWorkgroupRequest req, CancellationToken ct)
    {
        var wg = await service.GetByIdAsync(id, scheduleId, ct);
        wg.Name = req.Name; wg.Description = req.Description; wg.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(wg, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static WorkgroupResponse Map(Workgroup w) => new(w.Id, w.ScheduleId, w.Name, w.Description, w.Active, w.LastModifiedDateTime.ToString("O"));
}
