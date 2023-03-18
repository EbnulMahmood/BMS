using BMS.CoreBusiness.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels
{
    public record DevTaskViewModelCreate
    {
        [Required]
        [StringLength(255, ErrorMessage = "Title is too long")]
        [DisplayName("Task")]
        public string? Title { get; set; }
        [Required]
        public DevTaskStatus Status { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        public string? UXDesignLink { get; set; }
        public string? Group { get; set; }
        [Required]
        public string? Responsible1Id { get; set; }
        public string? Responsible2Id { get; set; }
        [Required]
        public int Release { get; set; }
        [Required]
        public decimal? EstimatedHours { get; set; }
        public decimal? ActualHours { get; set; }
        public string? FRSMenuLink { get; set; }
        public string? UrlOrMenuOrWorkflow { get; set; }
        public string? Remarks { get; set; }
        public DateTimeOffset TaskCompletedTime { get; set; }
        public DateTimeOffset TaskCreationDate { get; set; } = DateTimeOffset.Now;
        [Required]
        public string? QAResponsible { get; set; }
        [Required]
        public DateTimeOffset QADoneTime { get; set; }
        public string? Review { get; set; }
        public string? ReviewRemarks { get; set; }
        public string? TestCaseFunctional { get; set; }
        public string? TestFeatureAndScenario { get; set; }
        public string? WebRequestKey { get; set; }
    }

    public sealed record DevTaskViewModelEdit : DevTaskViewModelCreate
    {
        public Guid Id { get; set; }
    }
}
