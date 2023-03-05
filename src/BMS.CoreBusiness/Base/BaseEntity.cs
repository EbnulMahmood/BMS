using BMS.CoreBusiness.Enums;
using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.Base
{
    public abstract class BaseEntity<TId> : IEntity
    {
        [Required]
        public TId Id { get; protected init; }
        object IEntity.Id
        {
            get { return Id; }
            init { }
        }

        [Required]
        public TId CreatedBy { get; protected init; }
        object IEntity.CreatedBy
        {
            get { return CreatedBy; }
            init { }
        }

        [Required]
        public DateTimeOffset CreatedOnUtc { get; init; }
        public TId LastModifiedBy { get; protected init; }
        object IEntity.LastModifiedBy
        {
            get { return LastModifiedBy; }
            init { }
        }

        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string IPAddress { get; init; } = string.Empty;
        public Status Status { get; init; }
        [Timestamp]
        public byte[] RowVersion { get; init; }
    }
}
