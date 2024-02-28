using GoodReads.Core.Messages;

namespace GoodReads.Books.Application.Events
{
    public class BookDeletedEvent : DomainEvent
    {
        public Guid Id { get; private set; }

        public BookDeletedEvent(Guid id)
        {
            AggregateId = id;
            Id = id;
        }
    }
}
