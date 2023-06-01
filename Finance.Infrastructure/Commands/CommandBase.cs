using Finance.Domain._Core.Response;
using FluentValidation.Results;
using MediatR;
using System;
using System.Linq;

namespace Finance.Infrastructure.Commands
{
    public class CommandBase : IRequest<ResponseModel<bool>>
    {
        public ValidationResult _validationResult { get; set; }

        public virtual ResponseModel<bool> IsValid()
        {
            throw new NotImplementedException();
        }

        public ResponseModel<bool> Verify()
        {
            var result = new ResponseModel<bool>();
            if (!_validationResult.IsValid)
            {
                result.AddError(true, string.Join(",", _validationResult.Errors.Select(x => x.ErrorMessage)));
            };

            return result;
        }
    }
}
