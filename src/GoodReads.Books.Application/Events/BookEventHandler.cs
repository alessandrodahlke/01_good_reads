using MediatR;

namespace GoodReads.Books.Application.Events
{
    public class BookEventHandler : INotificationHandler<BookCreatedEvent>,
                                    INotificationHandler<BookUpdatedEvent>,
                                    INotificationHandler<BookDeletedEvent>
    {
        public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Publicar evento de integração

            return Task.CompletedTask;
        }

        public Task Handle(BookUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //Publicar evento de integração

            return Task.CompletedTask;
        }

        public Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            //Publicar evento de integração

            return Task.CompletedTask;
        }
    }
}
