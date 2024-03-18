using GoodReads.Core.DomainObjects;
using GoodReads.Core.DomainObjects.ValueObjects;

namespace GoodReadas.Users.Domain.Entities
{
    public class User : Entity, IAgreggateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public User(string name, string email)
        {
            Name = name;
            Email = new Email(email);
        }

        protected User() { }

    }
}
