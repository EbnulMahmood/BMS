using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels.Membership
{
    public sealed record RoleViewModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
