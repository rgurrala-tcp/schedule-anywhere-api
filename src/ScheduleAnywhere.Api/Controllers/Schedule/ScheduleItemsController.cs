using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/items")]
[Authorize]
public class ScheduleItemsController(IScheduleItemService service) : ControllerBase
{
    /// <summary>Returns paged schedule items. Supports date range and employee filters.</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<ScheduleItemResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 50,
        [FromQuery] string? from = null, [FromQuery] string? to = null,
        [FromQuery] int? employeeId = null, CancellationToken ct = default)
    {
        DateTime? fromDate = from is not null ? DateTime.Parse(from) : null;
        DateTime? toDate   = to   is not null ? DateTime.Parse(to)   : null;
        var result = await service.GetPagedAsync(scheduleId, page, pageSize, fromDate, toDate, employeeId, ct);
        return Ok(new PagedResponse<ScheduleItemResponse>(
            result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    /// <summary>Returns a single schedule item by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ScheduleItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    /// <summary>Creates a schedule item (assigns a shift or time-off entry to an employee).</summary>
    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ScheduleItemResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateScheduleItemRequest request, CancellationToken ct)
    {
        var item = new ScheduleItem
        {
            ScheduleId = scheduleId,
            EmployeeId = request.EmployeeId,
            ShiftId = request.ShiftId,
            ExplanationId = request.ExplanationId,
            Date = DateTime.Parse(request.Date),
            Notes = request.Notes,
            PaidHours = request.PaidHours,
            UnpaidHours = request.UnpaidHours
        };
        var created = await service.CreateAsync(item, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    /// <summary>Updates an existing schedule item (shift, explanation, notes, hours).</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ScheduleItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateScheduleItemRequest request, CancellationToken ct)
    {
        var item = await service.GetByIdAsync(id, scheduleId, ct);
        item.ShiftId = request.ShiftId;
        item.ExplanationId = request.ExplanationId;
        item.Notes = request.Notes;
        item.PaidHours = request.PaidHours;
        item.UnpaidHours = request.UnpaidHours;
        return Ok(Map(await service.UpdateAsync(item, ct)));
    }

    /// <summary>Deletes (erases) a schedule item.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static ScheduleItemResponse Map(ScheduleItem i) => new(
        i.Id, i.ScheduleId, i.EmployeeId, i.ShiftId, i.ExplanationId,
        i.Date.ToString("yyyy-MM-dd"), i.Notes, i.PaidHours, i.UnpaidHours,
        i.LastModifiedDateTime.ToString("O"));
}
