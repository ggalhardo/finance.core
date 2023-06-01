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
            RuleFor(c => c._paymentRequest.description)
                .NotEmpty()
                .WithMessage("Invalid description");

            RuleFor(c => c._paymentRequest.amount)
                .GreaterThan(0)
                .WithMessage("Invalid amount");

            RuleFor(c => c._paymentRequest.paymentType)
                .NotEmpty()
                .WithMessage("Invalid type");

            RuleFor(c => c._paymentRequest.paymentType)
                .Must(x => Enum.TryParse(x, true, out PaymentTypeEnum paymentTypeEnum))
                .WithMessage("Type was not found");
        }
    }
}
