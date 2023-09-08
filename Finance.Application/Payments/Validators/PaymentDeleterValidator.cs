using Finance.Application.Payments.Commands;
using FluentValidation;

namespace Finance.Application.Payments.Validators
{
    public class PaymentDeleterValidator : AbstractValidator<PaymentDeleterCommand>
    {
        public PaymentDeleterValidator()
        {
            RuleFor(c => c.PaymentId)
                .NotEmpty()
                .WithMessage("Invalid PaymentId");
        }
    }
}