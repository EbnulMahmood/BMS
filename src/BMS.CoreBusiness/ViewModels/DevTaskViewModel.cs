using BMS.CoreBusiness.BusinessRules;
using BMS.CoreBusiness.Enums;
using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels
{
    public record DevTaskViewModelCreate
    {
        [Display(Name = "Task")]
        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(ViewModelConstants.TaskSize, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string? Title { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Display(Name = "Project")]
        [Required]
        public Guid ProjectId { get; set; }
        public string? UXDesignLink { get; set; }
        public string? Group { get; set; }
        [Display(Name = "Responsible 1")]
        [Required]
        public string? Responsible1Id { get; set; }
        public string? Responsible2Id { get; set; }
        [Required]
        [Range(ViewModelConstants.ReleaseMinSize, int.MaxValue, ErrorMessage = "{0} can't be less than {1}.")]
        public int Release { get; set; }
        public string? FRSMenuLink { get; set; }
        public string? UrlOrMenuOrWorkflow { get; set; }
        public string? Remarks { get; set; }
        [Display(Name = "QA Responsible")]
        [Required]
        public string? QAResponsibleId { get; set; }
        public string? TestCaseFunctional { get; set; }
        public string? TestFeatureAndScenario { get; set; }
        public string? WebRequestKey { get; set; }
    }

    public sealed record DevTaskViewModelEdit : DevTaskViewModelCreate
    {
        public Guid Id { get; set; }
    }
}
