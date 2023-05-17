using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels
{
    public record ProjectViewModelCreate
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }
    }

    public sealed record ProjectViewModelEdit : ProjectViewModelCreate
    {
        [Required]
        public Guid Id { get; set; }
    }

    public sealed record ProjectViewModelDetils : ProjectViewModelCreate
    {
        public string? CreatedBy { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        public string? LastModifiedBy { get; init; }
        public DateTimeOffset? LastModifiedOnUtc { get; init; }
    }
}
