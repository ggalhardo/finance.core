using Finance.Application.Payments.Commands;
using Finance.Domain._Core.Enum;
using FluentValidation;
using System;

namespace Finance.Application.Payments.Validators
{
    public class PaymentCreatorValidator : AbstractValidator<PaymentCreatorCommand>
    {
        public PaymentCreatorValidator()
        {
            RuleFor(c => c.PaymentRequest.Description)
                .NotEmpty()
                .WithMessage("Invalid description");

            RuleFor(c => c.PaymentRequest.Amount)
                .GreaterThan(0)
                .WithMessage("Invalid amount");

            RuleFor(c => c.PaymentRequest.PaymentType)
                .NotEmpty()
                .WithMessage("Invalid type");

            RuleFor(c => c.PaymentRequest.PaymentType)
                .Must(x => Enum.TryParse(x, true, out PaymentTypeEnum paymentTypeEnum))
                .WithMessage("Type was not found");
        }
    }
}
