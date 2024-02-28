using GoodReads.Core.Messages;

namespace GoodReads.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

        public void AddEvent(DomainEvent eventItem)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveEvent(DomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void RemoveAllEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
