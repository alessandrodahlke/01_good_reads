using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class User : Document, IAgreggateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public List<Reading> Readings { get; private set; } = new();
        public List<Rating> Ratings { get; private set; } = new();

        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public void AddReading(Reading reading)
        {
            Readings.Add(reading);
        }

        public void AddRating(Rating rating)
        {
            Ratings.Add(rating);
        }
    }
}
