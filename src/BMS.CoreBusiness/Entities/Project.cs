using BMS.CoreBusiness.Base;

namespace BMS.CoreBusiness.Entities
{
    public sealed record Project : BaseEntity<Guid>, IEntity
    {
        public string? Name { get; set; }
    }
}
