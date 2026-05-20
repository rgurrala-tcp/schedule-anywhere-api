namespace ScheduleAnywhere.Core.Domain;

public enum Role
{
    ManageOrganizations,
    ManageOrganizationEmployees,
    ManageOrganizationLocations,
    ManageOrganizationDepartments,
    ManageOrganizationPositions,
    ManageOrganizationExplanations,
    ManageUserSchedules,
    ViewUserSchedules,
    ViewPersonalSchedule,
    RequestTimeOff,
    Administrator,
    ChangeEmployeeProfile,
    ScheduleRequest,
    ReceiveEmailNotifications,
    SelfScheduling
}

public enum CostType { Hourly, Salary, None }

public enum ShiftViewType { Standard, TimeOff, Hidden }

public enum OrganizationStatus { Active, Inactive, Trial }

public enum ImportStatus { Pending, Processing, Completed, Failed }
