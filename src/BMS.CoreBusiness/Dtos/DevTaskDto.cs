using BMS.CoreBusiness.Enums;

namespace BMS.CoreBusiness.Dtos
{
    public readonly record struct DevTaskDto(Guid Id, string? Title, DevTaskStatus Status, Priority Priority, string? Project, string? UXDesignLink, string? Group, string? CreatedBy, string? LastModifiedBy, string? Responsible1, string? Responsible2, int Release, decimal? EstimatedHours, decimal? ActualHours, string? FRSMenuLink, string? UrlOrMenuOrWorkflow, string? Remarks, DateTimeOffset TaskCompletedTime, DateTimeOffset TaskCreationDate, string? QAResponsible, DateTimeOffset QADoneTime, string? Review, string? ReviewRemarks,  string? TestCaseFunctional, string? TestFeatureAndScenario, string? WebRequestKey);
}
