using BMS.CoreBusiness.Enums;
using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.ViewModels
{
    public sealed class ProjectViewModelCreate
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name is too long")]
        public string? Name { get; set; }
        public Guid CreatedById { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        public Guid LastModifiedById { get; init; }
        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string IPAddress { get; init; }
        public Status Status { get; init; }
    }
}
