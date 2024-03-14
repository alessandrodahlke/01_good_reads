using GoodReads.Core.Mediator;
using GoodReads.Core.Messages.Integration;
using MassTransit;

namespace GoodReads.Books.Infra.Rabbitmq.Consumers
{
    public class AverageGradeCalculatedConsumer : IConsumer<AverageGradeCalculatedIntegrationEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AverageGradeCalculatedConsumer(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Consume(ConsumeContext<AverageGradeCalculatedIntegrationEvent> context)
        {
           await _mediatorHandler.Publish(context.Message);
        }
    }
}
