using FluentValidation;
using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Commands
{
    public class CreateReadingCommand : Command
    {
        public CreateReadingCommand(Guid bookId, Guid userId, DateTime startedDate, DateTime? endedDate)
        {
            BookId = bookId;
            UserId = userId;
            StartedDate = startedDate;
            EndedDate = endedDate;
        }

        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateReadingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateReadingCommandValidation : AbstractValidator<CreateReadingCommand>
    {
        public CreateReadingCommandValidation()
        {
            RuleFor(c => c.BookId)
                .NotEqual(Guid.Empty)
                .WithMessage("The book id is required");

            RuleFor(c => c.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The user id is required");

            When(c => c.EndedDate.HasValue, () =>
            {
                RuleFor(c => c.EndedDate)
                    .GreaterThan(c => c.StartedDate)
                    .WithMessage("The ended date must be greater than the started date");
            });

        }
    }
}
