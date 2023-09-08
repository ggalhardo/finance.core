using Finance.Application.Payments.Commands;
using FluentValidation;

namespace Finance.Application.Payments.Validators
{
    public class PaymentUpdaterValidator : AbstractValidator<PaymentUpdaterCommand>
    {
        public PaymentUpdaterValidator()
        {
            RuleFor(c => c.PaymentId)
                .NotEmpty()
                .WithMessage("Invalid PaymentId");
        }
    }
}
