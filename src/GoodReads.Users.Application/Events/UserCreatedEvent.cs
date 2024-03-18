using GoodReads.Core.Messages;

namespace GoodReads.Users.Application.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
