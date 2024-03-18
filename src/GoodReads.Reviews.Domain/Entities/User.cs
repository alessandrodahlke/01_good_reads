using GoodReads.Core.DomainObjects;

namespace GoodReads.Reviews.Domain.Entities
{
    public class User : IAgreggateRoot
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public List<Book> Books { get; private set; } = new();
        public List<Review> Reviews { get; private set; } = new();
        public List<Rating> Ratings { get; private set; } = new();
        public List<Reading> Readings { get; private set; } = new();

        public User(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public void AddRading(Reading reading)
        {
            Readings.Add(reading);
        }
    }
}
