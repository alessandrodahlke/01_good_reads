using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Review : IAgreggateRoot
    {
        public string Id { get; private set; }
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public string UserId { get; private set; }
        public string BookId { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public Review(int grade, string description, string userId, string bookId)
        {
            Id = Guid.NewGuid().ToString();
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
            CreatedAt = DateTime.Now;
        }
    }
}
