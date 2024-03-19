using FluentValidation;
using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Commands
{
    public class CreateRatingCommand : Command
    {
        public int Grade { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }

        public CreateRatingCommand(int grade, string description, Guid userId, Guid bookId)
        {
            Grade = grade;
            Description = description;
            UserId = userId;
            BookId = bookId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateRatingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateRatingCommandValidation : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidation()
        {
            RuleFor(c => c.Grade)
                .InclusiveBetween(0, 5)
                .WithMessage("The grade must be between 0 and 5");

            RuleFor(c => c.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The user id is required");

            RuleFor(c => c.BookId)
                .NotEqual(Guid.Empty)
                .WithMessage("The book id is required");

        }
    }
}
