using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodReads.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult { get; set; } = new ValidationResult();

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected string[] GetErrors()
        {
            return ValidationResult.Errors.Select(x => x.ErrorMessage).ToArray();
        }
    }
}
