using BMS.CoreBusiness.Base;

namespace BMS.CoreBusiness.Entities
{
    public sealed class Project : BaseEntity<Guid>, IEntity
    {
        public string? Name { get; set; }
    }
}
