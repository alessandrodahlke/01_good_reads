using GoodReads.Core.Mediator;
using GoodReads.Core.Messages.Integration;
using MassTransit;

namespace GoodReads.Reviews.Infra.Rabbitmq.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UserCreatedConsumer(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            await _mediatorHandler.Publish(context.Message);
        }
    }
}
