using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class ReviewCreatedEventHandler : INotificationHandler<ReviewCreatedEvent>
    {
        public async Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
