using FluentValidation;
using GoodReads.Core.DomainObjects.ValueObjects;
using GoodReads.Core.Messages;

namespace GoodReads.Users.Application.Commands
{
    public class CreateUserCommand : Command
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public CreateUserCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("The name is required");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("The email is required")
                .EmailAddress()
                .WithMessage("The email is in an invalid format")
                .MinimumLength(Email.AddressMinLength)
                .MaximumLength(Email.AddressMaxLength)
                .WithMessage("The email must have between {MinLength} and {MaxLength} characters");
        }
    }
}
