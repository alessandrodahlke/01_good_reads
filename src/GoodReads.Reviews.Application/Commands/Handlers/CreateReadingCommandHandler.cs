using GoodReads.Core.Mediator;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Events;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Commands.Handlers
{
    public class CreateReadingCommandHandler : CommandHandler,
        IRequestHandler<CreateReadingCommand, CustomResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReadingRepository _readingRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CreateReadingCommandHandler(IUserRepository userRepository,
                                           IBookRepository bookRepository,
                                           IReadingRepository readingRepository,
                                           IMediatorHandler mediatorHandler)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _readingRepository = readingRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CustomResult> Handle(CreateReadingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            var user = await _userRepository.GetById(message.UserId.ToString());
            if (user == null)
                return CustomResult.Failure("The user does not exist", GetErrors());

            var book = await _bookRepository.GetById(message.BookId.ToString());
            if (book == null)
                return CustomResult.Failure("The book does not exist", GetErrors());

            var reading = new Reading(book.Id, user.Id, message.StartedDate, message.EndedDate);

            await _readingRepository.Add(reading);

            await _mediatorHandler.Publish(new ReadingCreatedEvent(reading.Id, book.Id, user.Id, reading.StartedDate, reading.EndedDate));

            return CustomResult.Success("Reading created successfully", reading);
        }
    }
}
