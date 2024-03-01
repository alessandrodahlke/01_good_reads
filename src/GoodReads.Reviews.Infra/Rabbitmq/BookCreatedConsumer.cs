using GoodReads.Core.Messages.Integration;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodReads.Reviews.Infra.Rabbitmq
{
    public class BookCreatedConsumer : IConsumer<BookCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<BookCreatedIntegrationEvent> context)
        {
           await Task.CompletedTask;
        }
    }
}
