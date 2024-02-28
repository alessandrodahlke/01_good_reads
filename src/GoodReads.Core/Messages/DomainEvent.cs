namespace GoodReads.Core.Messages
{
    public abstract class DomainEvent : Event
    {
        public Guid AggregateId { get; set; }
    }
}
