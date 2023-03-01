using BMS.CoreBusiness.Enums;

namespace BMS.CoreBusiness.ViewModels
{
    public class DevTaskViewModelCreate
    {
        public string? Title { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string? Project { get; set; }
        public string? UXDesignLink { get; set; }
        public string? Group { get; set; }
        public string? EntryBy { get; set; }
        public string? Responsible1 { get; set; }
        public string? Responsible2 { get; set; }
        public int Release { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? ActualHours { get; set; }
        public string? FRSMenuLink { get; set; }
        public string? UrlOrMenuOrWorkflow { get; set; }
        public string? Remarks { get; set; }
        public DateTimeOffset TaskCompletedTime { get; set; }
        public DateTimeOffset TaskCreationDate { get; set; }
        public string? QAResponsible { get; set; }
        public DateTimeOffset QADoneTime { get; set; }
        public string? Review { get; set; }
        public string? ReviewRemarks { get; set; }
        public string? TestCaseFunctional { get; set; }
        public string? TestFeatureAndScenario { get; set; }
        public string? WebRequestKey { get; set; }
    }

    public sealed class DevTaskViewModelEdit : DevTaskViewModelCreate
    {
        public Guid Id { get; set; }
    }
}
