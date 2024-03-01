using GoodReads.Core.Messages;

namespace GoodReads.Core.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : IntegrationEvent;
    }
}
