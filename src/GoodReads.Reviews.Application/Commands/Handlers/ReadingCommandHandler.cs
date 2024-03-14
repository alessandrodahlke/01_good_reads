using GoodReads.Core.Data;
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
        private readonly IUnitOfWork _unitOfWork;

        public ReadingCommandHandler(IReadingRepository readingRepository,
                                     IMediatorHandler mediatorHandler,
                                     IUnitOfWork unitOfWork)
        {
            _readingRepository = readingRepository;
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> Handle(CreateReadingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            //var user = await _userRepository.GetById(message.UserId);

            //if (user == null)
            //    return CustomResult.Failure("The user does not exist", GetErrors());

            //var book = await _bookRepository.GetById(message.BookId);
            //if (book == null)
            //    return CustomResult.Failure("The book does not exist", GetErrors());

            var reading = new Reading(message.BookId, message.UserId, message.StartedDate, message.EndedDate);

            //await _readingRepository.Add(reading);

            //var result = await _unitOfWork.Commit();

            //if (result)
            //    await _mediatorHandler.PublicarEvento(new ReadingCreatedEvent(reading.Id, reading.BookId, reading.UserId, reading.StartedDate, reading.EndedDate));

            return CustomResult.Success("Reading created successfully", reading);
        }
    }
}
