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
    public class RatingCommandHandler : CommandHandler,
        IRequestHandler<CreateRatingCommand, CustomResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;

        public RatingCommandHandler(IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediatorHandler)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CustomResult> Handle(CreateRatingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            var rating = new Rating(message.Grade, message.UserId.ToString(), message.BookId.ToString());

            var book = await _bookRepository.GetById(rating.BookId.ToString());

            book.AddRating(rating);

            await _bookRepository.Update(book);

            _bookRepository.AddRating(message.BookId.ToString(), rating);

            var result = await _unitOfWork.Commit();

            if (result)
                await _mediatorHandler.Publish(new RatingCreatedEvent(rating.Id!, rating.Grade, rating.UserId, rating.BookId));

            return CustomResult.Success("Rating created successfully", rating);
        }
    }
}
