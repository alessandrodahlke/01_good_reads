﻿using GoodReads.Core.Data;
using GoodReads.Core.Mediator;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using GoodReads.Reviews.Application.Events;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Commands.Handlers
{
    public class ReviewCommandHandler : CommandHandler,
        IRequestHandler<CreateReviewCommand, CustomResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediatorHandler _mediatorHandler;

        public ReviewCommandHandler(IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IMediatorHandler mediatorHandler)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<CustomResult> Handle(CreateReviewCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return CustomResult.Failure("Invalid Command", message.GetErrors());

            var review = new Review(message.Grade, message.Description, message.UserId.ToString(), message.BookId.ToString());

            //var book = await _bookRepository.GetById(review.BookId.ToString());

            //book.AddReview(review);

            //await _bookRepository.Update(book);

            _bookRepository.AddReview(message.BookId.ToString(), review);

            var result = await _unitOfWork.Commit();

            if (result)
                await _mediatorHandler.PublicarEvento(new ReviewCreatedEvent(review.Id!, review.Grade, review.Description, review.UserId, review.BookId));

            return CustomResult.Success("Review created successfully", review);
        }
    }
}
