using GoodReadas.Users.Domain.Entities;
using GoodReadas.Users.Domain.Repositories;
using GoodReads.Core.Messages;
using GoodReads.Core.Results;
using GoodReads.Users.Application.Events;
using MediatR;

namespace GoodReads.Users.Application.Commands.Handlers
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, CustomResult>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                return CustomResult.Failure("Invalid command", request.GetErrors());

            var userExists = await _userRepository.GetByEmail(request.Email);
            if (userExists != null)
                return CustomResult.Failure("User already exists");

            var user = new User(request.Name, request.Email);
            await _userRepository.Add(user);

            user.AddEvent(new UserCreatedEvent(user.Id, user.Name, user.Email.Address));

            var result = await _userRepository.UnitOfWork.Commit();

            if (result)
                return CustomResult.Success("User created successfully", user.Id);

            return CustomResult.Failure("Error creating user");
        }
    }
}
