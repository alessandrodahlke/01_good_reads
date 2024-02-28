using FluentValidation;
using GoodReads.Core.Messages;

namespace GoodReads.Books.Application.Commands
{
    public class DeleteBookCommand : Command
    {
        public Guid Id { get; private set; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public string[] GetErrors()
        {
            return ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray();
        }

        public class DeleteBookCommandValidation : AbstractValidator<DeleteBookCommand>
        {
            public DeleteBookCommandValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("The id field must be filled");
            }
        }
    }
}
