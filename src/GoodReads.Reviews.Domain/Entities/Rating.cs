using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Rating : Document
    {
        public Rating(int grade, string description, string userId, string bookId)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
        }

        public int Grade { get; private set; }
        public string Description { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }
    }
}
