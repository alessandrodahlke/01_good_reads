using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Reading : IAgreggateRoot
    {
        public string Id { get; private set; }
        public Book Book { get; private set; }
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public Reading(Book book, DateTime startedDate, DateTime? endedDate)
        {
            Id = Guid.NewGuid().ToString();
            Book = book;
            StartedDate = startedDate;
            EndedDate = endedDate;

            if (EndedDate.HasValue && EndedDate.Value < StartedDate)
            {
                throw new DomainException("Ended date must be greater than started date");
            }
        }
    }
}
