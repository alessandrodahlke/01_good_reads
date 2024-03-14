namespace GoodReads.Core.Messages.Integration
{
    public class AverageGradeCalculatedIntegrationEvent : IntegrationEvent
    {
        public Guid BookId { get; private set; }
        public decimal Average { get; private set; }

        public AverageGradeCalculatedIntegrationEvent(Guid bookId, decimal average)
        {
            BookId = bookId;
            Average = average;
        }
    }
}
