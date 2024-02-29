using MediatR;

namespace GoodReads.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid AggregateId { get; set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
