using ScheduleAnywhere.Core.Common;
using ScheduleAnywhere.Core.Domain;
using ScheduleAnywhere.Core.Exceptions;
using ScheduleAnywhere.Core.Interfaces;
using ScheduleAnywhere.Services.Interfaces;

namespace ScheduleAnywhere.Services.Implementations;

public class OpenShiftService(IRepository<OpenShift> shiftRepo, IRepository<OpenShiftRequest> requestRepo) : IOpenShiftService
{
    public Task<PagedResult<OpenShift>> GetPagedAsync(int scheduleId, int page, int pageSize, DateTime? date, CancellationToken ct) =>
        shiftRepo.GetPagedAsync(page, pageSize, o => o.ScheduleId == scheduleId && (date == null || o.Date.Date == date.Value.Date), ct);

    public async Task<OpenShift> GetByIdAsync(int id, int scheduleId, CancellationToken ct)
    {
        var os = await shiftRepo.GetByIdAsync(id, ct);
        if (os is null || os.ScheduleId != scheduleId)
            throw new NotFoundException(nameof(OpenShift), id);
        return os;
    }

    public async Task<OpenShift> CreateAsync(OpenShift openShift, CancellationToken ct)
    {
        openShift.LastModifiedDateTime = DateTime.UtcNow;
        return await shiftRepo.AddAsync(openShift, ct);
    }

    public async Task<OpenShift> UpdateAsync(OpenShift openShift, CancellationToken ct)
    {
        openShift.LastModifiedDateTime = DateTime.UtcNow;
        await shiftRepo.UpdateAsync(openShift, ct);
        return openShift;
    }

    public async Task DeleteAsync(int id, int scheduleId, CancellationToken ct)
    {
        var os = await GetByIdAsync(id, scheduleId, ct);
        await shiftRepo.DeleteAsync(os, ct);
    }

    public async Task<OpenShiftRequest> SubmitRequestAsync(int openShiftId, int employeeId, string? notes, CancellationToken ct)
    {
        var request = new OpenShiftRequest
        {
            OpenShiftId = openShiftId,
            EmployeeId = employeeId,
            Notes = notes,
            RequestedAt = DateTime.UtcNow
        };
        return await requestRepo.AddAsync(request, ct);
    }

    public async Task ApproveRequestAsync(int requestId, int reviewerEmployeeId, CancellationToken ct)
    {
        var request = await requestRepo.GetByIdAsync(requestId, ct)
            ?? throw new NotFoundException(nameof(OpenShiftRequest), requestId);
        request.IsApproved = true;
        request.ReviewedAt = DateTime.UtcNow;
        await requestRepo.UpdateAsync(request, ct);
    }

    public async Task DenyRequestAsync(int requestId, int reviewerEmployeeId, CancellationToken ct)
    {
        var request = await requestRepo.GetByIdAsync(requestId, ct)
            ?? throw new NotFoundException(nameof(OpenShiftRequest), requestId);
        request.IsApproved = false;
        request.ReviewedAt = DateTime.UtcNow;
        await requestRepo.UpdateAsync(request, ct);
    }
}
