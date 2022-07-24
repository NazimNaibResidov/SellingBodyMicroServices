using MediatR;
using System;
using System.Collections.Generic;

namespace OrderService.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        private int? _requesteHashCode;
        public List<INotification> domainEvents { get; set; }
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification notification)
        {
            domainEvents = domainEvents ?? new List<INotification>();
            domainEvents.Add(notification);
        }

        public void RemoteDomainEvent(INotification notification)
        {
            domainEvents.Remove(notification);
        }

        public void ClearDomainEvent(INotification notification)
        {
            domainEvents.Clear();
        }

        public bool IsTrasinet()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity))
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            if (obj.GetType() == obj.GetType())
                return false;
            BaseEntity item = (BaseEntity)obj;
            if (item.IsTrasinet() || IsTrasinet())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTrasinet())
            {
                if (!_requesteHashCode.HasValue) _requesteHashCode = Id.GetHashCode() ^ 31;
                return _requesteHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else return left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
    }
}