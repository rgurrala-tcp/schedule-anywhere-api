using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Schedule;

[ApiController]
[Route("api/schedules/{scheduleId:int}/explanations")]
[Authorize]
public class ExplanationsController(IExplanationService service) : ControllerBase
{
    /// <summary>Returns paged explanations (time-off codes) for a schedule.</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<ExplanationResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int scheduleId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(scheduleId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<ExplanationResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<ExplanationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int scheduleId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, scheduleId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageOrganizationExplanations")]
    [ProducesResponseType<ExplanationResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int scheduleId, [FromBody] CreateExplanationRequest req, CancellationToken ct)
    {
        var exp = new Explanation { ScheduleId = scheduleId, Name = req.Name, Abbreviation = req.Abbreviation, Description = req.Description, Active = req.Active, IsTimeOff = req.IsTimeOff };
        var created = await service.CreateAsync(exp, ct);
        return CreatedAtAction(nameof(GetById), new { scheduleId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationExplanations")]
    [ProducesResponseType<ExplanationResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int scheduleId, int id, [FromBody] UpdateExplanationRequest req, CancellationToken ct)
    {
        var exp = await service.GetByIdAsync(id, scheduleId, ct);
        exp.Name = req.Name; exp.Abbreviation = req.Abbreviation;
        exp.Description = req.Description; exp.Active = req.Active; exp.IsTimeOff = req.IsTimeOff;
        return Ok(Map(await service.UpdateAsync(exp, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationExplanations")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int scheduleId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, scheduleId, ct);
        return NoContent();
    }

    private static ExplanationResponse Map(Explanation e) => new(
        e.Id, e.ScheduleId, e.Name, e.Abbreviation, e.Description,
        e.Active, e.IsTimeOff, e.LastModifiedDateTime.ToString("O"));
}
