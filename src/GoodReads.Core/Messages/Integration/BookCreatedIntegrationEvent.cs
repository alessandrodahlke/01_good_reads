namespace GoodReads.Core.Messages.Integration
{
    public class BookCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }

        public BookCreatedIntegrationEvent(Guid id, string title,string description, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
        }
    }
}
