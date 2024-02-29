using GoodReads.Reviews.Application.DTO;
using GoodReads.Reviews.Domain.Repositories;

namespace GoodReads.Reviews.Application.Queries
{
    public class ReadingQueries : IReadingQueries
    {
        private readonly IReadingRepository _readingRepository;

        public ReadingQueries(IReadingRepository reviewRepository)
        {
            _readingRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReadingDTO>> GetReadingByUserIdAsync(Guid userId)
        {
            var readings = await _readingRepository.GetByUserId(userId);

            return readings.Select(r => new ReadingDTO
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                StartedDate = r.StartedDate,
                EndedDate = r.EndedDate,
            });
        }
    }
}
