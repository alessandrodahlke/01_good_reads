using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Events
{
    public class ReviewCreatedEvent : DomainEvent
    {
        public ReviewCreatedEvent(Guid id, int grade, string description, Guid userId, Guid bookId)
        {
            Id = id;
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
        }

        public Guid Id { get; private set; }
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }

    }
}