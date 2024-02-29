using FluentValidation.Results;
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

        public virtual string[] GetErrors()
        {
            throw new NotImplementedException();
        }
    }
}
