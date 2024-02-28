using GoodReads.Books.Domain.Enums;

namespace GoodReads.Books.Application.Commands
{
    public class CreateBookResponse
    {
        public CreateBookResponse(Guid id, string title, string description, string iSBN, string author, string publisher, EGenderBook gender, int year, int numberOfPages)
        {
            Id = id;
            Title = title;
            Description = description;
            ISBN = iSBN;
            Author = author;
            Publisher = publisher;
            Gender = gender;
            Year = year;
            NumberOfPages = numberOfPages;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public EGenderBook Gender { get; private set; }
        public int Year { get; private set; }
        public int NumberOfPages { get; private set; }
    }
}
