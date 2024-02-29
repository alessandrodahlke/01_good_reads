using GoodReads.Reviews.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodReads.Reviews.Application.Queries
{
    public interface IReadingQueries
    {
        Task<IEnumerable<ReadingDTO>> GetReadingByUserIdAsync(Guid userId);
    }
}
