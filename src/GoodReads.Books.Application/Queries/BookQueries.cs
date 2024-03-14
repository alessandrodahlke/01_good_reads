using GoodReads.Books.Application.DTO;
using GoodReads.Books.Domain.Repositories;
using GoodReads.Core.Results;

namespace GoodReads.Books.Application.Queries
{
    public class BookQueries : IBookQueries
    {
        private readonly IBookRepository _bookRepository;
        public BookQueries(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<PagedResult<BookDTO>> GetAll(int pageSize, int pageIndex)
        {
            var books = await _bookRepository.GetAll(pageSize, pageIndex);

            var booksDTO = books.Items.Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ISBN = b.ISBN,
                Author = b.Author,
                Publisher = b.Publisher,
                Gender = b.Gender,
                Year = b.Year,
                NumberOfPages = b.NumberOfPages,
                AverageGrade = b.AverageGrade
            });

            return new PagedResult<BookDTO>
            {
                Items = booksDTO,
                PageIndex = books.PageIndex,
                PageSize = books.PageSize,
                TotalResults = books.TotalResults
            };
        }

        public async Task<BookDTO> GetById(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                ISBN = book.ISBN,
                Author = book.Author,
                Publisher = book.Publisher,
                Gender = book.Gender,
                Year = book.Year,
                NumberOfPages = book.NumberOfPages,
                AverageGrade = book.AverageGrade
            };
        }
    }
}
