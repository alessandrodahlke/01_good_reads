using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Rating : IAgreggateRoot
    {
        public string Id { get; private set; }
        public int Grade { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Rating(int grade, string userId, string bookId)
        {
            Id = Guid.NewGuid().ToString();
            Grade = grade;
            UserId = userId;
            BookId = bookId;
            CreatedAt = DateTime.Now;
        }
    }
}
