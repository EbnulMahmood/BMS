﻿using BMS.CoreBusiness.Entities;
using BMS.CoreBusiness.Enums;

namespace BMS.CoreBusiness.Base
{
    public interface IEntity
    {
        object Id { get; init; }
        object CreatedById { get; init; }
        public DateTimeOffset CreatedOnUtc { get; init; }
        object LastModifiedById { get; init; }
        public DateTimeOffset? LastModifiedOnUtc { get; init; }
        public string IPAddress { get; init; }
        public Status Status { get; init; }
        public byte[] RowVersion { get; init; }
    }
}
