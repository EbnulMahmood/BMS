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
        public TId CreatedById { get; protected init; }
        object IEntity.CreatedById
        {
            get { return CreatedById; }
            init { }
        }

        [Required]
        public DateTimeOffset CreatedOnUtc { get; init; }
        public TId LastModifiedById { get; protected init; }
        object IEntity.LastModifiedById
        {
            get { return LastModifiedById; }
            init { }
        }

        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string IPAddress { get; init; } = string.Empty;
        public Status Status { get; init; }
        [Timestamp]
        public byte[] RowVersion { get; init; }
    }
}
