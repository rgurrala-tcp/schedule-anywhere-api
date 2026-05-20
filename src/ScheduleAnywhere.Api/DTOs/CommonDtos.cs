namespace ScheduleAnywhere.Api.DTOs;

/// <summary>Paged response envelope returned by all list endpoints.</summary>
public record PagedResponse<T>(
    IReadOnlyList<T> Items,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages,
    bool HasPreviousPage,
    bool HasNextPage);

/// <summary>Common query parameters for list endpoints.</summary>
public record PageQuery(int Page = 1, int PageSize = 25, bool? Active = null, string? Search = null);

// ── Auth ─────────────────────────────────────────────────────────────────────

public record LoginRequest(string Username, string Password);
public record RefreshRequest(string RefreshToken);
public record LoginResponse(string AccessToken, string RefreshToken, string ExpiresAt, int EmployeeId, int OrganizationId, IReadOnlyList<string> Roles);

// ── Employee ─────────────────────────────────────────────────────────────────

public record EmployeeResponse(
    int Id, int OrganizationId, int DepartmentId, int LocationId, int PositionId,
    int? SupervisorEmployeeId, string FirstName, string LastName, string Username,
    string? Email1, string? Email2, string? Phone1, string? Mobile,
    string? ExportIdentifier, string? Comments,
    bool Active, bool CanLogin,
    string CreatedAt, string HireDate, string? DeactivatedAt, string? LastLoginAt,
    string LastModifiedAt, decimal? Cost, string? CostType);

public record CreateEmployeeRequest(
    int DepartmentId, int LocationId, int PositionId, int? SupervisorEmployeeId,
    string FirstName, string LastName, string Username, string Password,
    string? Email1, string? Email2, string? Phone1, string? Mobile,
    string? ExportIdentifier, string? Comments,
    bool Active, bool CanLogin, string HireDate,
    decimal? Cost, string? CostType);

public record UpdateEmployeeRequest(
    int DepartmentId, int LocationId, int PositionId, int? SupervisorEmployeeId,
    string FirstName, string LastName,
    string? Email1, string? Email2, string? Phone1, string? Mobile,
    string? ExportIdentifier, string? Comments,
    bool Active, bool CanLogin,
    decimal? Cost, string? CostType);

// ── Schedule ──────────────────────────────────────────────────────────────────

public record ScheduleResponse(int Id, int OrganizationId, string Name, string? Description,
    bool Active, bool IsPosted, string? PostedDate, string LastModifiedAt);

public record CreateScheduleRequest(string Name, string? Description, bool Active);
public record UpdateScheduleRequest(string Name, string? Description, bool Active);

// ── ScheduleItem ──────────────────────────────────────────────────────────────

public record ScheduleItemResponse(int Id, int ScheduleId, int EmployeeId, int? ShiftId,
    int? ExplanationId, string Date, string? Notes, decimal? PaidHours, decimal? UnpaidHours,
    string LastModifiedAt);

public record CreateScheduleItemRequest(int EmployeeId, int? ShiftId, int? ExplanationId,
    string Date, string? Notes, decimal? PaidHours, decimal? UnpaidHours);

public record UpdateScheduleItemRequest(int? ShiftId, int? ExplanationId,
    string? Notes, decimal? PaidHours, decimal? UnpaidHours);

// ── Shift ─────────────────────────────────────────────────────────────────────

public record ShiftResponse(int Id, int ScheduleId, int ColorId, int? ParentShiftId,
    string Name, string Abbreviation, string? Description, string? Code,
    bool Active, bool IsTimeOff, string ShiftView,
    string StartTime, string StopTime, int BreakMinutes,
    decimal? PaidHours, decimal? UnpaidHours, string LastModifiedAt);

public record CreateShiftRequest(int ColorId, int? ParentShiftId, string Name, string Abbreviation,
    string? Description, string? Code, bool Active, bool IsTimeOff, string ShiftView,
    string StartTime, string StopTime, int BreakMinutes, decimal? PaidHours, decimal? UnpaidHours);

public record UpdateShiftRequest(int ColorId, int? ParentShiftId, string Name, string Abbreviation,
    string? Description, string? Code, bool Active, bool IsTimeOff, string ShiftView,
    string StartTime, string StopTime, int BreakMinutes, decimal? PaidHours, decimal? UnpaidHours);

// ── Explanation ───────────────────────────────────────────────────────────────

public record ExplanationResponse(int Id, int ScheduleId, string Name, string Abbreviation,
    string? Description, bool Active, bool IsTimeOff, string LastModifiedAt);

public record CreateExplanationRequest(string Name, string Abbreviation, string? Description,
    bool Active, bool IsTimeOff);

public record UpdateExplanationRequest(string Name, string Abbreviation, string? Description,
    bool Active, bool IsTimeOff);

// ── Department / Location / Position ─────────────────────────────────────────

public record DepartmentResponse(int Id, int OrganizationId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreateDepartmentRequest(string Name, string? Description, bool Active);
public record UpdateDepartmentRequest(string Name, string? Description, bool Active);

public record LocationResponse(int Id, int OrganizationId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreateLocationRequest(string Name, string? Description, bool Active);
public record UpdateLocationRequest(string Name, string? Description, bool Active);

public record PositionResponse(int Id, int OrganizationId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreatePositionRequest(string Name, string? Description, bool Active);
public record UpdatePositionRequest(string Name, string? Description, bool Active);

// ── Skill ─────────────────────────────────────────────────────────────────────

public record SkillResponse(int Id, int OrganizationId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreateSkillRequest(string Name, string? Description, bool Active);
public record UpdateSkillRequest(string Name, string? Description, bool Active);

// ── Filter ────────────────────────────────────────────────────────────────────

public record FilterResponse(int Id, int ScheduleId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreateFilterRequest(string Name, string? Description, bool Active);
public record UpdateFilterRequest(string Name, string? Description, bool Active);

// ── Workgroup ─────────────────────────────────────────────────────────────────

public record WorkgroupResponse(int Id, int ScheduleId, string Name, string? Description,
    bool Active, string LastModifiedAt);

public record CreateWorkgroupRequest(string Name, string? Description, bool Active);
public record UpdateWorkgroupRequest(string Name, string? Description, bool Active);

// ── CoverageWatch ─────────────────────────────────────────────────────────────

public record CoverageWatchResponse(int Id, int ScheduleId, string Name, bool Active,
    string LastModifiedAt);

public record CreateCoverageWatchRequest(string Name, bool Active);
public record UpdateCoverageWatchRequest(string Name, bool Active);

// ── OpenShift ─────────────────────────────────────────────────────────────────

public record OpenShiftResponse(int Id, int ScheduleId, int ShiftId, string Date,
    bool IsActive, string LastModifiedAt);

public record CreateOpenShiftRequest(int ShiftId, string Date);
public record UpdateOpenShiftRequest(int ShiftId, string Date, bool IsActive);

public record SubmitOpenShiftRequestRequest(string? Notes);
