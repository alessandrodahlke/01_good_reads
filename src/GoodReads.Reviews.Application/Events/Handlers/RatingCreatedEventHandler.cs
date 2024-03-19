using GoodReads.Core.Data;
using GoodReads.Core.DomainObjects;
using GoodReads.Core.MessageBus;
using GoodReads.Core.Messages.Integration;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class RatingCreatedEventHandler : INotificationHandler<RatingCreatedEvent>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageBus _messageBus;
        public RatingCreatedEventHandler(IRatingRepository ratingRepository,
                                         IBookRepository bookRepository,
                                         IUserRepository userRepository,
                                         IMessageBus messageBus)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _messageBus = messageBus;
        }
        public async Task Handle(RatingCreatedEvent notification, CancellationToken cancellationToken)
        {
            await CalculateAverageGrade(notification, cancellationToken);

            await AddRatingUser(notification, cancellationToken);
        }

        private async Task AddRatingUser(RatingCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(notification.UserId);
            if (user is null)
                throw new DomainException("The user does not exist");

            var rating = await _ratingRepository.GetById(notification.Id);

            user.AddRating(rating);

            await _userRepository.Update(user);

        }

        private async Task CalculateAverageGrade(RatingCreatedEvent notification, CancellationToken cancellationToken)
        {
            var ratings = await _ratingRepository.GetByBookId(notification.BookId);

            var averageGrade = ratings.Any() ? (decimal)ratings.Average(r => r.Grade) : 0;

            var book = await _bookRepository.GetById(notification.BookId);
            if (book is null)
                throw new DomainException("The book does not exist");

            book.UpdateAverageGrade(averageGrade);

            await _bookRepository.Update(book);

            await _messageBus.PublishAsync(
                new AverageGradeCalculatedIntegrationEvent(Guid.Parse(notification.BookId), averageGrade),
                cancellationToken);
        }
    }
}
