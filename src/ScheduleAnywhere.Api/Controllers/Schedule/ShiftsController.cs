using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/shifts")]
[Authorize]
public class ShiftsController(IShiftService service) : ControllerBase
{
    /// <summary>Returns a paged list of shifts in a schedule.</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<ShiftResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(scheduleId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<ShiftResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    /// <summary>Returns a shift by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ShiftResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    /// <summary>Creates a new shift. Name must be unique within the schedule.</summary>
    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ShiftResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateShiftRequest req, CancellationToken ct)
    {
        var shift = new Shift
        {
            ScheduleId = scheduleId, ColorId = req.ColorId, ParentShiftId = req.ParentShiftId,
            Name = req.Name, Abbreviation = req.Abbreviation, Description = req.Description,
            Code = req.Code, Active = req.Active, IsTimeOff = req.IsTimeOff,
            ShiftView = Enum.Parse<ShiftViewType>(req.ShiftView),
            StartTime = DateTime.Parse(req.StartTime), StopTime = DateTime.Parse(req.StopTime),
            BreakMinutes = req.BreakMinutes, PaidHours = req.PaidHours, UnpaidHours = req.UnpaidHours
        };
        var created = await service.CreateAsync(shift, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    /// <summary>Updates an existing shift.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<ShiftResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateShiftRequest req, CancellationToken ct)
    {
        var shift = await service.GetByIdAsync(id, scheduleId, ct);
        shift.ColorId = req.ColorId; shift.ParentShiftId = req.ParentShiftId;
        shift.Name = req.Name; shift.Abbreviation = req.Abbreviation;
        shift.Description = req.Description; shift.Code = req.Code;
        shift.Active = req.Active; shift.IsTimeOff = req.IsTimeOff;
        shift.ShiftView = Enum.Parse<ShiftViewType>(req.ShiftView);
        shift.StartTime = DateTime.Parse(req.StartTime); shift.StopTime = DateTime.Parse(req.StopTime);
        shift.BreakMinutes = req.BreakMinutes; shift.PaidHours = req.PaidHours; shift.UnpaidHours = req.UnpaidHours;
        return Ok(Map(await service.UpdateAsync(shift, ct)));
    }

    /// <summary>Deletes a shift. Cannot delete a shift with assigned schedule items.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static ShiftResponse Map(Shift s) => new(
        s.Id, s.ScheduleId, s.ColorId, s.ParentShiftId, s.Name, s.Abbreviation,
        s.Description, s.Code, s.Active, s.IsTimeOff, s.ShiftView.ToString(),
        s.StartTime.ToString("O"), s.StopTime.ToString("O"), s.BreakMinutes,
        s.PaidHours, s.UnpaidHours, s.LastModifiedDateTime.ToString("O"));
}
