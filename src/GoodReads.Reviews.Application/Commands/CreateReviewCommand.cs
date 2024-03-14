﻿using FluentValidation;
using GoodReads.Core.Messages;

namespace GoodReads.Reviews.Application.Commands
{
    public class CreateReviewCommand : Command
    {
        public string Description { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }

        public CreateReviewCommand(string description, Guid userId, Guid bookId)
        {
            Description = description;
            UserId = userId;
            BookId = bookId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateReviewCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CreateReviewCommandValidation : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidation()
        {
            RuleFor(c=>c.Description)
                .NotEmpty()
                .WithMessage("The description is required");

            RuleFor(c=>c.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage("The user id is required");

            RuleFor(c=>c.BookId)
                .NotEqual(Guid.Empty)
                .WithMessage("The book id is required");

        }
    }
}
