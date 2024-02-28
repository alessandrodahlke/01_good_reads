using FluentValidation;
using GoodReads.Books.Domain.Enums;
using GoodReads.Core.Messages;

namespace GoodReads.Books.Application.Commands
{
    public class CreateBookCommand : Command
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public string Publisher { get; private set; }
        public EGenderBook Gender { get; private set; }
        public int Year { get; private set; }
        public int NumberOfPages { get; private set; }

        public CreateBookCommand(string title, string description, string author, string isbn, string publisher, EGenderBook gender, int year, int numberOfPages)
        {
            Title = title;
            Description = description;
            Author = author;
            ISBN = isbn;
            Publisher = publisher;
            Gender = gender;
            Year = year;
            NumberOfPages = numberOfPages;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public string[] GetErrors()
        {
            return ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray();
        }

        public class CreateBookCommandValidation : AbstractValidator<CreateBookCommand>
        {
            public CreateBookCommandValidation()
            {
                RuleFor(c => c.Title)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("The title field must be filled");

                RuleFor(c => c.Author)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("The author field must be filled");

                RuleFor(c => c.ISBN)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("The ISBN field must be filled");

                RuleFor(c => c.Publisher)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("The publisher field must be filled");

                RuleFor(c => c.Gender)
                    .IsInEnum();

                RuleFor(c=>c.Year)
                    .GreaterThan(0)
                    .WithMessage("The year field must be greater than 0");

                RuleFor(c => c.NumberOfPages)
                    .GreaterThan(0)
                    .WithMessage("The number of pages field must be greater than 0");
            }
        }
    }
}
