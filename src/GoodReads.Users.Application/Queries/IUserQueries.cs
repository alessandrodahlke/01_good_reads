using GoodReads.Users.Application.DTO;

namespace GoodReads.Users.Application.Queries
{
    public interface IUserQueries
    {
        Task<UserDTO> GetUserById(Guid id);
    }
}
