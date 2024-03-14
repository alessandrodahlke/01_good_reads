using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class Book : IAgreggateRoot
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal AverageGrade { get; private set; }
        public string Author { get; private set; }

        public List<Review> Reviews { get; private set; } = new();
        public List<Rating> Ratings { get; private set; } = new();

        public Book(string id, string title, string description, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
        }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }

        public void AddRating(Rating rating)
        {
            Ratings.Add(rating);
            CalculateAverageGrade();
        }

        public void CalculateAverageGrade()
        {
            if (Ratings.Count == 0)
                return;

            AverageGrade = (decimal)Ratings.Select(r => r.Grade).Average();
        }
    }
}
