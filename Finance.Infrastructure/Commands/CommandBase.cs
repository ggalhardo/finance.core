using Finance.Domain._Core.Response;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;

namespace Finance.Infrastructure.Commands
{
    public class CommandBase : IRequest<ResponseModel<string>>
    {
        public ValidationResult ValidationResult { get; private set; }

        public virtual ResponseModel<string> IsValid()
        {
            throw new NotImplementedException();
        }

        public void SetValidation(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }

        public ResponseModel<string> Verify()
        {
            var result = new ResponseModel<string>();
            if (!ValidationResult.IsValid)
            {
                result.AddError(string.Join(",", ValidationResult.Errors.Select(x => x.ErrorMessage)));
            }

            return result;
        }
    }
}
