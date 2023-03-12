using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels
{
    public sealed record ProjectViewModelCreate
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }
        public Guid CreatedById { get; set; }
        public DateTimeOffset CreatedOnUtc { get; set; }
        public Guid LastModifiedById { get; set; }
        public DateTimeOffset? LastModifiedOnUtc { get; set; }
        public string IPAddress { get; set; }
    }
}
