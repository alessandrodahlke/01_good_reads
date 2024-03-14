using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using MediatR;

namespace GoodReads.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CustomResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task Publish<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }
    }
}
