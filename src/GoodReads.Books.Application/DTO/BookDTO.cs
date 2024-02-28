using GoodReads.Books.Domain.Enums;

namespace GoodReads.Books.Application.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public EGenderBook Gender { get; set; }
        public int Year { get; set; }
        public int NumberOfPages { get; set; }
        public decimal AverageGrade { get; set; }
    }
}
