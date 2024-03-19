using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetById(string id);
        Task Update(User book);
    }
}
