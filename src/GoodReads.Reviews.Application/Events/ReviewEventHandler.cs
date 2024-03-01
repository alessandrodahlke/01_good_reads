using MediatR;

namespace GoodReads.Reviews.Application.Events
{
    public class ReviewEventHandler : INotificationHandler<ReviewCreatedEvent>
    {
        public async Task Handle(ReviewCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Calcular nota media e publicar evento de integração para demais contextos

            await Task.CompletedTask;
        }
    }
}
