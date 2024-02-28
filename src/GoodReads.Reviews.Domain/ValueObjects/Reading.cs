using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.ValueObjects
{
    public class Reading
    {
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public Reading(DateTime startedDate, DateTime? endedDate)
        {
            StartedDate = startedDate;
            EndedDate = endedDate;

            if (EndedDate.HasValue && EndedDate.Value < StartedDate)
            {
                throw new DomainException("Ended date must be greater than started date");
            }
        }
    }
}
