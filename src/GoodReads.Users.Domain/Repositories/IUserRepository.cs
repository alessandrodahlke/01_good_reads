using GoodReadas.Users.Domain.Entities;
using GoodReads.Core.Data;

namespace GoodReadas.Users.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task Add(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
    }
}
