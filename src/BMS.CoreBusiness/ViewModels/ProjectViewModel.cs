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
    }
}
