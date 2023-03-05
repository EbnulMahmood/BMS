using BMS.CoreBusiness.Enums;

namespace BMS.CoreBusiness.Base
{
    public interface IEntity
    {
        object Id { get; init; }
        object CreatedBy { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        object LastModifiedBy { get; init; }
        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string IPAddress { get; init; }
        public Status Status { get; init; }
        public byte[] RowVersion { get; init; }
    }
}
