using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;

namespace ScheduleAnywhere.Services.Interfaces;

public interface IOpenShiftService
{
    Task<PagedResult<OpenShift>> GetPagedAsync(int scheduleId, int page, int pageSize, DateTime? date = null, CancellationToken ct = default);
    Task<OpenShift> GetByIdAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<OpenShift> CreateAsync(OpenShift openShift, CancellationToken ct = default);
    Task<OpenShift> UpdateAsync(OpenShift openShift, CancellationToken ct = default);
    Task DeleteAsync(int id, int scheduleId, CancellationToken ct = default);
    Task<OpenShiftRequest> SubmitRequestAsync(int openShiftId, int employeeId, string? notes, CancellationToken ct = default);
    Task ApproveRequestAsync(int requestId, int reviewerEmployeeId, CancellationToken ct = default);
    Task DenyRequestAsync(int requestId, int reviewerEmployeeId, CancellationToken ct = default);
}
