using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IReadingRepository
    {
        Task Add(Reading reading);
        Task<Reading> GetById(string id);
        Task Update(Reading reading);

        Task<IEnumerable<Reading>> GetByBookId(string bookId);
        Task<IEnumerable<Reading>> GetByUserId(string userId);
        Task<Reading> GetByUserAndBook(string userId, string bookId);
    }
}
