using Finance.Application.Payments.Commands;
using FluentValidation;

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
        }
    }
}
