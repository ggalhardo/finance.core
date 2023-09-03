using Finance.Domain._Core.Response;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;

namespace Finance.Infrastructure.Commands
{
    public class CommandBase : IRequest<ResponseModel<bool>>
    {
        public ValidationResult ValidationResult { get; private set; }

        public virtual ResponseModel<bool> IsValid()
        {
            throw new NotImplementedException();
        }

        public void SetValidation(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }

        public ResponseModel<bool> Verify()
        {
            var result = new ResponseModel<bool>();
            if (!ValidationResult.IsValid)
            {
                result.AddError(true, string.Join(",", ValidationResult.Errors.Select(x => x.ErrorMessage)));
            };

            return result;
        }
    }
}
