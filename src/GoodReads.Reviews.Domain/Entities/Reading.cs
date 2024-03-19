using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Reading : Document
    {
        public string BookId { get; private set; }
        public string UserId { get; private set; }
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public Reading(string bookId, string userId, DateTime startedDate, DateTime? endedDate)
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
