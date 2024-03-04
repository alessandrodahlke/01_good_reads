using GoodReads.Books.Domain.Enums;
using GoodReads.Core.Messages;

namespace GoodReads.Books.Application.Events
{
    public class BookCreatedEvent : DomainEvent
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public string Publisher { get; private set; }
        public EGenderBook Gender { get; private set; }
        public int Year { get; private set; }
        public int NumberOfPages { get; private set; }


        public BookCreatedEvent(Guid id, string title, string description,string author, string isbn, string publisher, EGenderBook gender, int year, int numberOfPages)
        {
            AggregateId = id;
            Id = id;
            Title = title;
            Description = description;
            Author = author;
            ISBN = isbn;
            Publisher = publisher;
            Gender = gender;
            Year = year;
            NumberOfPages = numberOfPages;
        }
    }
}
