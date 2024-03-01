using GoodReads.Core.DomainObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Review : IAgreggateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public Review(int grade, string description, Guid userId, Guid bookId)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
            CreatedAt = DateTime.Now;
        }
    }
}
