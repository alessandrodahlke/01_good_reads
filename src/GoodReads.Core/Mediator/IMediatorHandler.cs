using GoodReads.Core.Messages;
using GoodReads.Core.Results;

namespace GoodReads.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<CustomResult> EnviarComando<T>(T command) where T : Command;
        Task PublicarEvento<T>(T @event) where T : Event;
    }
}
