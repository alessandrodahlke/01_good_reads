namespace GoodReads.Core.DomainObjects.ValueObjects
{
    public class Email : ValueObject
    {
        public const int AddressMaxLength = 254;
        public const int AddressMinLength = 5;

        public string Address { get; private set; }


        public Email(string address)
        {
            if (!IsValid(address)) throw new DomainException("Invalid email");

            Address = address;
        }

        public static bool IsValid(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            var emailRegex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailRegex.IsMatch(email);
        }
    }
}
