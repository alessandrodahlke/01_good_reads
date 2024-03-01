using GoodReads.Core.Messages;
using MassTransit;

namespace GoodReads.Core.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync<T>(T message) where T : IntegrationEvent;
    }

    public class MessageBus : IMessageBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MessageBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            await _publishEndpoint.Publish(message);
        }
    }
}
