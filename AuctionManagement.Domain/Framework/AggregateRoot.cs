using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AuctionManagement.Domain.Contracts;

namespace AuctionManagement.Domain.Framework
{
    public abstract class AggregateRoot
    {
        private List<DomainEvent> UncommittedChanges;
        public AggregateRoot()
        {
            this.UncommittedChanges = new List<DomainEvent>();
        }

        public void Publish(DomainEvent @event)
        {
            this.UncommittedChanges.Add(@event);
        }

        public ReadOnlyCollection<DomainEvent> GetChanges()
        {
            return this.UncommittedChanges.AsReadOnly();
        }
        public void Causes(DomainEvent @event)
        {
            this.Apply(@event);
            this.Publish(@event);
        }
        public abstract void Apply(DomainEvent @event);
    }
}
