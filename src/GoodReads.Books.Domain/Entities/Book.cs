using GoodReads.Books.Domain.Enums;
using GoodReads.Core.DomainObjects;

namespace GoodReads.Books.Domain.Entities
{
    public class Book : Entity, IAgreggateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ISBN { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public EGenderBook Gender { get; private set; }
        public int Year { get; private set; }
        public int NumberOfPages { get; private set; }
        public decimal AverageGrade { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // EF Relation
        protected Book() { }

        public Book(string title,string description, string isbn, string author, string publisher, EGenderBook eGender, int year, int numberOfPages)
        {
            Title = title;
            Description = description;
            ISBN = isbn;
            Author = author;
            Publisher = publisher;
            Gender = eGender;
            Year = year;
            NumberOfPages = numberOfPages;
        }

        public void UpdateBook(string title, string description, string author, string publisher, EGenderBook gender, int year, int numberOfPage)
        {
            Title = title;
            Description = description;
            Author = author;
            Publisher = publisher;
            Gender = gender;
            Year = year;
            NumberOfPages = numberOfPage;
        }

        public void UpdateAverageGrade(decimal averageGrade)
        {
            AverageGrade = averageGrade;
        }
    }
}
