namespace GoodReads.Core.Messages.Integration
{
    public class AverageGradeCalculatedEvent : IntegrationEvent
    {
        public Guid BookId { get; private set; }
        public decimal Average { get; private set; }

        public AverageGradeCalculatedEvent(Guid bookId, decimal average)
        {
            BookId = bookId;
            Average = average;
        }
    }
}
