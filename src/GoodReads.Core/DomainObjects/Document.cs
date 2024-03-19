namespace GoodReads.Core.DomainObjects
{
    public abstract class Document
    {
        public virtual string Id { get; set;}
        public DateTime CreatedAt { get; set; }

        protected Document()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
