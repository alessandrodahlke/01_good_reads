using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class ReviewEventHandler : INotificationHandler<ReviewCreatedEvent>
    {
        public async Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
