using GoodReads.Core.Mediator;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Commands.Handlers
{
    public class ReadingCommandHandler : CommandHandler,
        IRequestHandler<CreateReadingCommand, CustomResult>
    {
        private readonly IReadingRepository _readingRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public ReadingCommandHandler(IReadingRepository readingRepository,
                                     IMediatorHandler mediatorHandler)
        {
            _readingRepository = readingRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CustomResult> Handle(CreateReadingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            var reading = new Reading(message.BookId, message.UserId, message.StartedDate, message.EndedDate);

            await _readingRepository.Add(reading);

            return CustomResult.Success("Reading created successfully", reading);
        }
    }
}
