using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Organization;

[ApiController]
[Route("api/organizations/{organizationId:int}/employees")]
[Authorize]
public class EmployeesController(IEmployeeService service, ICurrentUserService currentUser) : ControllerBase
{
    /// <summary>Returns a paged list of employees. Supports filtering by active status and name search.</summary>
    [HttpGet]
    [ProducesResponseType<PagedResponse<EmployeeResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int organizationId, [FromQuery] PageQuery q, CancellationToken ct)
    {
        var result = await service.GetPagedAsync(organizationId, q.Page, q.PageSize, q.Active, q.Search, ct);
        return Ok(new PagedResponse<EmployeeResponse>(
            result.Items.Select(Map).ToList(),
            result.TotalCount, result.Page, result.PageSize, result.TotalPages,
            result.HasPreviousPage, result.HasNextPage));
    }

    /// <summary>Returns a single employee by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<EmployeeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int organizationId, int id, CancellationToken ct) =>
        Ok(Map(await service.GetByIdAsync(id, organizationId, ct)));

    /// <summary>Creates a new employee.</summary>
    [HttpPost]
    [Authorize(Roles = "Administrator,ManageOrganizationEmployees")]
    [ProducesResponseType<EmployeeResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(int organizationId, [FromBody] CreateEmployeeRequest request, CancellationToken ct)
    {
        var employee = new Employee
        {
            OrganizationId = organizationId,
            DepartmentId = request.DepartmentId,
            LocationId = request.LocationId,
            PositionId = request.PositionId,
            SupervisorEmployeeId = request.SupervisorEmployeeId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            PasswordHash = request.Password,
            Email1 = request.Email1,
            Email2 = request.Email2,
            Phone1 = request.Phone1,
            Mobile = request.Mobile,
            ExportIdentifier = request.ExportIdentifier,
            Comments = request.Comments,
            Active = request.Active,
            CanLogin = request.CanLogin,
            HireDate = DateTime.Parse(request.HireDate),
            Cost = request.Cost,
            CostType = request.CostType is not null ? Enum.Parse<CostType>(request.CostType) : null
        };
        var created = await service.CreateAsync(employee, ct);
        return CreatedAtAction(nameof(GetById), new { organizationId, id = created.Id }, Map(created));
    }

    /// <summary>Updates an existing employee.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationEmployees")]
    [ProducesResponseType<EmployeeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int organizationId, int id, [FromBody] UpdateEmployeeRequest request, CancellationToken ct)
    {
        var employee = await service.GetByIdAsync(id, organizationId, ct);
        employee.DepartmentId = request.DepartmentId;
        employee.LocationId = request.LocationId;
        employee.PositionId = request.PositionId;
        employee.SupervisorEmployeeId = request.SupervisorEmployeeId;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email1 = request.Email1;
        employee.Email2 = request.Email2;
        employee.Phone1 = request.Phone1;
        employee.Mobile = request.Mobile;
        employee.ExportIdentifier = request.ExportIdentifier;
        employee.Comments = request.Comments;
        employee.Active = request.Active;
        employee.CanLogin = request.CanLogin;
        employee.Cost = request.Cost;
        employee.CostType = request.CostType is not null ? Enum.Parse<CostType>(request.CostType) : null;
        return Ok(Map(await service.UpdateAsync(employee, ct)));
    }

    /// <summary>Deletes an employee. Active employees with schedule items cannot be deleted.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator,ManageOrganizationEmployees")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int organizationId, int id, CancellationToken ct)
    {
        await service.DeleteAsync(id, organizationId, ct);
        return NoContent();
    }

    private static EmployeeResponse Map(Employee e) => new(
        e.Id, e.OrganizationId, e.DepartmentId, e.LocationId, e.PositionId,
        e.SupervisorEmployeeId, e.FirstName, e.LastName, e.Username,
        e.Email1, e.Email2, e.Phone1, e.Mobile,
        e.ExportIdentifier, e.Comments,
        e.Active, e.CanLogin,
        e.Created.ToString("O"), e.HireDate.ToString("O"),
        e.Deactivated?.ToString("O"), e.LastLogin?.ToString("O"),
        e.LastModifiedDateTime.ToString("O"), e.Cost, e.CostType?.ToString());
}
