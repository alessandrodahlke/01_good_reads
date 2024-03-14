using GoodReads.Core.Messages;
using GoodReads.Core.Results;

namespace GoodReads.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<CustomResult> SendCommand<T>(T command) where T : Command;
        Task Publish<T>(T @event) where T : Event;
    }
}
