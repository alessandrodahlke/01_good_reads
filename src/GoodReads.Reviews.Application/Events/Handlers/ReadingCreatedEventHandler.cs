using GoodReads.Core.DomainObjects;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class ReadingCreatedEventHandler : INotificationHandler<ReadingCreatedEvent>
    {
        private readonly IReadingRepository _readingRepository;
        private readonly IUserRepository _userRepository;
        public ReadingCreatedEventHandler(IReadingRepository readingRepository,
                                          IUserRepository userRepository)
        {
            _readingRepository = readingRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(ReadingCreatedEvent notification, CancellationToken cancellationToken)
        {
            var reading = await _readingRepository.GetById(notification.Id);
            if (reading is null)
                throw new DomainException("The reading does not exist");

            var user = await _userRepository.GetById(notification.UserId);
            if (user is null)
                throw new DomainException("The user does not exist");

            user.AddReading(reading);

            await _userRepository.Update(user);
        }
    }
}
