using FluentValidation.Results;
using GoodReads.Core.Results;
using MediatR;

namespace GoodReads.Core.Messages
{
    public abstract class Command : Message, IRequest<CustomResult>
    {
        protected DateTime Timestamp { get; private set; }

        protected ValidationResult ValidationResult { get; set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        public string[] GetErrors()
        {
            return ValidationResult.Errors.Select(e => e.ErrorMessage).ToArray();
        }
    }
}
