using GoodReads.Core.DomainObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Reading
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public Reading(Guid bookId, Guid userId, DateTime startedDate, DateTime? endedDate)
        {
            BookId = bookId;
            UserId = userId;
            StartedDate = startedDate;
            EndedDate = endedDate;

            if (EndedDate.HasValue && EndedDate.Value < StartedDate)
            {
                throw new DomainException("Ended date must be greater than started date");
            }
        }
    }
}
