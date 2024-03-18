using GoodReadas.Users.Domain.Repositories;
using GoodReads.Users.Application.DTO;

namespace GoodReads.Users.Application.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
                return null;

            return new UserDTO(user.Id, user.Name, user.Email.Address);
        }
    }
}
