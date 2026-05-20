using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/open-shifts")]
[Authorize]
public class OpenShiftsController(IOpenShiftService service) : ControllerBase
{
    /// <summary>Returns open shifts. Optionally filter by date (yyyy-MM-dd).</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<OpenShiftResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] int page = 1, [FromQuery] int pageSize = 25, [FromQuery] string? date = null, CancellationToken ct = default)
    {
        DateTime? dateFilter = date is not null ? DateTime.Parse(date) : null;
        var result = await service.GetPagedAsync(scheduleId, page, pageSize, dateFilter, ct);
        return Ok(new PagedResponse<OpenShiftResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<OpenShiftResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<OpenShiftResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateOpenShiftRequest req, CancellationToken ct)
    {
        var os = new OpenShift { ScheduleId = scheduleId, ShiftId = req.ShiftId, Date = DateTime.Parse(req.Date), IsActive = true };
        var created = await service.CreateAsync(os, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType<OpenShiftResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateOpenShiftRequest req, CancellationToken ct)
    {
        var os = await service.GetByIdAsync(id, scheduleId, ct);
        os.ShiftId = req.ShiftId; os.Date = DateTime.Parse(req.Date); os.IsActive = req.IsActive;
        return Ok(Map(await service.UpdateAsync(os, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    /// <summary>Submits a request for an employee to claim an open shift.</summary>
    [HttpPost("{id:int}/requests")]
    [Authorize(Roles = "ScheduleRequest,SelfScheduling")]
    public async Task<IActionResult> SubmitRequest(int scheduleId, int id,
        [FromBody] SubmitOpenShiftRequestRequest req,
        [FromServices] ICurrentUserService currentUser, CancellationToken ct)
    {
        var request = await service.SubmitRequestAsync(id, currentUser.EmployeeId, req.Notes, ct);
        return Ok(request.Id);
    }

    /// <summary>Approves an open-shift request.</summary>
    [HttpPost("{id:int}/requests/{requestId:int}/approve")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    public async Task<IActionResult> ApproveRequest(int scheduleId, int id, int requestId,
        [FromServices] ICurrentUserService currentUser, CancellationToken ct)
    {
        await service.ApproveRequestAsync(requestId, currentUser.EmployeeId, ct);
        return NoContent();
    }

    /// <summary>Denies an open-shift request.</summary>
    [HttpPost("{id:int}/requests/{requestId:int}/deny")]
    [Authorize(Roles = "Administrator,ManageUserSchedules")]
    public async Task<IActionResult> DenyRequest(int scheduleId, int id, int requestId,
        [FromServices] ICurrentUserService currentUser, CancellationToken ct)
    {
        await service.DenyRequestAsync(requestId, currentUser.EmployeeId, ct);
        return NoContent();
    }

    private static OpenShiftResponse Map(OpenShift o) => new(o.Id, o.ScheduleId, o.ShiftId, o.Date.ToString("yyyy-MM-dd"), o.IsActive, o.LastModifiedDateTime.ToString("O"));
}
