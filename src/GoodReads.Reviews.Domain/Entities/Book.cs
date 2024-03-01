using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Book : IAgreggateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }

        public List<Review> Reviews { get; set; }

        public Book(string title, string description, string author)
        {
            Title = title;
            Description = description;
            Author = author;
        }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }
    }
}
