using GoodReads.Core.MessageBus;
using GoodReads.Core.Messages.Integration;
using MediatR;

namespace GoodReads.Users.Application.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IMessageBus _messageBus;

        public UserCreatedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _messageBus.PublishAsync(
                new UserCreatedIntegrationEvent(notification.Id, notification.Name, notification.Email), cancellationToken);
        }
    }
}
