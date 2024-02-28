using GoodReads.Core.DomainObjects;
using GoodReads.Reviews.Domain.ValueObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; private set; }
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<Reading> Readings { get; private set; }


        public Review(int grade, string description, Guid userId, Guid bookId, DateTime createdAt, List<Reading> readings)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
            CreatedAt = createdAt;
            Readings = readings;
        }
    }
}
