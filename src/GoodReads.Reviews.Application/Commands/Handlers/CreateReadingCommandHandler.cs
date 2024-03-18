using GoodReads.Core.Data;
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
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReadingCommandHandler(IUserRepository userRepository,
                                           IBookRepository bookRepository,
                                           IMediatorHandler mediatorHandler,
                                           IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
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

            var reading = new Reading(book, message.StartedDate, message.EndedDate);

            user.AddRading(reading);

            await _userRepository.Update(user);

            var result = await _unitOfWork.Commit();

            if (result)
                await _mediatorHandler.Publish(new ReadingCreatedEvent(reading.Id, Guid.Parse(book.Id), Guid.Parse(user.Id), reading.StartedDate, reading.EndedDate));

            return CustomResult.Success("Reading created successfully", reading);
        }
    }
}
