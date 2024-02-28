using FluentValidation.Results;
using GoodReads.Core.Messages;

namespace GoodReads.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<CustomResult> EnviarComando<T>(T command) where T : Command;
        Task PublicarEvento<T>(T @event) where T : DomainEvent;
    }
}
