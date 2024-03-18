using GoodReads.Core.Data;
using GoodReads.Core.Messages.Integration;
using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MediatR;

namespace GoodReads.Reviews.Application.Events.Handlers
{
    public class UserCreatedIntegrationEventHandler : INotificationHandler<UserCreatedIntegrationEvent>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedIntegrationEventHandler(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            _UserRepository = UserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _UserRepository.Add(new User(notification.Id.ToString(),
                    notification.Name, notification.Email));

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
