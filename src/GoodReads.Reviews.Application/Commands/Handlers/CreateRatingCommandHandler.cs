using GoodReads.Core.Mediator;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Events;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Commands.Handlers
{
    public class CreateRatingCommandHandler : CommandHandler,
        IRequestHandler<CreateRatingCommand, CustomResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CreateRatingCommandHandler(IBookRepository bookRepository,
            IRatingRepository ratingRepository,
            IUserRepository userRepository,
            IMediatorHandler mediatorHandler)
        {
            _bookRepository = bookRepository;
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CustomResult> Handle(CreateRatingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            var user = await _userRepository.GetById(message.UserId.ToString());
            if (user == null)
                return CustomResult.Failure("The user does not exist", GetErrors());

            var book = await _bookRepository.GetById(message.BookId.ToString());
            if (book == null)
                return CustomResult.Failure("The book does not exist", GetErrors());

            var ratingExists = await _ratingRepository.GetByUserAndBook(user.Id, book.Id);
            if (ratingExists != null)
                return CustomResult.Failure("The user has already rated this book", GetErrors());

            var rating = new Rating(message.Grade, message.Description, user.Id, book.Id);

            await _ratingRepository.Add(rating);

            await _mediatorHandler.Publish(new RatingCreatedEvent(rating.Id!, rating.Grade, user.Id, book.Id));

            return CustomResult.Success("Rating created successfully", rating);
        }
    }
}
