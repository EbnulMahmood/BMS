namespace BMS.CoreBusiness.Base
{
    public interface IEntity
    {
        object Id { get; init; }
        object CreatedById { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        object LastModifiedById { get; set; }
        public DateTimeOffset? LastModifiedOnUtc { get; set; }
        public string IPAddress { get; init; }
        public byte[] RowVersion { get; init; }
        public bool IsDeleted { get; init; }
    }
}
