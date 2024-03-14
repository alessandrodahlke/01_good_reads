using GoodReads.Core.Mediator;
using GoodReads.Core.Messages.Integration;
using MassTransit;

namespace GoodReads.Reviews.Infra.Rabbitmq.Consumers
{
    public class BookCreatedConsumer : IConsumer<BookCreatedIntegrationEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public BookCreatedConsumer(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Consume(ConsumeContext<BookCreatedIntegrationEvent> context)
        {
            await _mediatorHandler.Publish(context.Message);
        }
    }
}
