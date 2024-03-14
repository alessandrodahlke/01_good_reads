using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Events
{
    public class RatingCreatedEvent : DomainEvent
    {
        public RatingCreatedEvent(string id, int grade, string userId, string bookId)
        {
            Id = id;
            Grade = grade;
            UserId = userId;
            BookId = bookId;
        }

        public string Id { get; private set; }
        public int Grade { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }

    }
}