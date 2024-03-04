using GoodReads.Core.MessageBus;
using GoodReads.Core.Messages.Integration;
using MassTransit;
using MediatR;

namespace GoodReads.Books.Application.Events
{
    public class BookEventHandler : INotificationHandler<BookCreatedEvent>,
                                    INotificationHandler<BookUpdatedEvent>,
                                    INotificationHandler<BookDeletedEvent>
    {
        private readonly IMessageBus _messageBus;

        public BookEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Publicar evento de integração
            await _messageBus.PublishAsync(new BookCreatedIntegrationEvent(notification.Id, notification.Title,notification.Description, notification.Author), cancellationToken);
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
