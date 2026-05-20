using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Organization;

[ApiController]
[Route("api/organizations/{organizationId:int}/departments")]
[Authorize]
public class DepartmentsController(IDepartmentService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResponse<DepartmentResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, ct);
        return Ok(new PagedResponse<DepartmentResponse>(result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<DepartmentResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    [HttpPost]
    [Authorize(Roles = "Administrator,ManageOrganizationDepartments")]
    [ProducesResponseType<DepartmentResponse>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreateDepartmentRequest req, CancellationToken ct)
    {
        var dept = new Department { OrganizationId = organizationId, Name = req.Name, Description = req.Description, Active = req.Active };
        var created = await service.CreateAsync(dept, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationDepartments")]
    [ProducesResponseType<DepartmentResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdateDepartmentRequest req, CancellationToken ct)
    {
        var dept = await service.GetByIdAsync(id, organizationId, ct);
        dept.Name = req.Name; dept.Description = req.Description; dept.Active = req.Active;
        return Ok(Map(await service.UpdateAsync(dept, ct)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationDepartments")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static DepartmentResponse Map(Department d) => new(d.Id, d.OrganizationId, d.Name, d.Description, d.Active, d.LastModifiedDateTime.ToString("O"));
}
