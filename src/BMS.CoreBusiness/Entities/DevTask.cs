using BMS.CoreBusiness.Base;
using BMS.CoreBusiness.Entities.Membership;
using BMS.CoreBusiness.Enums;

namespace BMS.CoreBusiness.Entities
{
    public sealed record DevTask : BaseEntity<Guid>, IEntity
    {
        public string? Title { get; init; }
        public DevTaskStatus Status { get; set; }
        public Priority Priority { get; init; }
        public Guid ProjectId { get; set; }
        public string? UXDesignLink { get; init; }
        public string? Group { get; init; }
        public string EntryById { get; set; } = string.Empty;
        public string? Responsible1Id { get; init; } = string.Empty;
        public string? Responsible2Id { get; init; }
        public int Release { get; init; }
        public decimal? EstimatedHours { get; init; }
        public decimal? ActualHours { get; init; }
        public string? FRSMenuLink { get; init; }
        public string? UrlOrMenuOrWorkflow { get; init; }
        public string? Remarks { get; init; }
        public DateTimeOffset TaskCompletedTime { get; init; }
        public DateTimeOffset TaskCreationDate { get; init; }
        public string? QAResponsibleId { get; init; }
        public DateTimeOffset QADoneTime { get; init; }
        public string? Review { get; init; }
        public string? ReviewRemarks { get; init; }
        public string? TestCaseFunctional { get; init; }
        public string? TestFeatureAndScenario { get; init; }
        public string? WebRequestKey { get; init; }
    }
}
