using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels.Membership
{
    public sealed record UserViewModel
    {
        [EmailAddress]
        [Required]
        public string LoginName { get; set; } = string.Empty;
        [Required]
        public string UserRole { get; set; } = string.Empty;
    }
}
