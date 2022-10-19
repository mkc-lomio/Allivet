using Allivet.WebAPI.Domain.Common.Interfaces;
using System;


namespace Allivet.WebAPI.Domain.Base
{
    public class EntityBase : Entity, IAggregateRoot
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime DateModified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
