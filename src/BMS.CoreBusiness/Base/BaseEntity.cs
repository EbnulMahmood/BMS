using System.ComponentModel.DataAnnotations;

namespace BMS.CoreBusiness.Base
{
    public abstract record BaseEntity<TId> : IEntity
    {
        [Required]
        public TId Id { get; init; }
        object IEntity.Id
        {
            get { return Id; }
            init { }
        }

        [Required]
        public TId CreatedById { get; init; }
        object IEntity.CreatedById
        {
            get { return CreatedById; }
            init { }
        }

        [Required]
        public DateTimeOffset CreatedOnUtc { get; init; }
        public TId LastModifiedById { get; set; }
        object IEntity.LastModifiedById
        {
            get { return LastModifiedById; }
            set { }
        }

        public DateTimeOffset? LastModifiedOnUtc { get; set; }
        public string IPAddress { get; init; } = string.Empty;
        [Timestamp]
        public byte[] RowVersion { get; init; }
        public bool IsDeleted { get; init; }
    }
}
