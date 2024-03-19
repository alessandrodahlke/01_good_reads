using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Events
{
    public class ReadingCreatedEvent : DomainEvent
    {
        public ReadingCreatedEvent(string id, string userId, string bookId, DateTime startedDate, DateTime? endedDate = null)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            StartedDate = startedDate;
            EndedDate = endedDate;
        }

        public string Id { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

    }
}