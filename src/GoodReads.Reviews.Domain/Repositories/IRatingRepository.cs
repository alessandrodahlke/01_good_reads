using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IRatingRepository
    {
        Task Add(Rating rating);
        Task<Rating> GetById(string id);
        Task Update(Rating rating);

        Task<IEnumerable<Rating>> GetByBookId(string bookId);
        Task<IEnumerable<Rating>> GetByUserId(string userId);
        Task<Rating> GetByUserAndBook(string userId, string bookId);
    }
}
