using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Events
{
    public class ReviewCreatedEvent : DomainEvent
    {
        public ReviewCreatedEvent(string id, int grade, string description, string userId, string bookId)
        {
            Id = id;
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
        }

        public string Id { get; private set; }
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }

    }
}