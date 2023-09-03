using Finance.Application.Payments.Validators;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Commands;

namespace Finance.Application.Payments.Commands
{
    public class PaymentCreatorCommand : CommandBase
    {
        public PaymentRequest PaymentRequest { get; private set; }

        public PaymentCreatorCommand(PaymentRequest paymentRequest)
        {
            this.PaymentRequest = paymentRequest;
        }

        public override ResponseModel<bool> IsValid()
        {
            base.SetValidation(new PaymentCreatorValidator().Validate(this));
            return base.Verify();
        }
    }
}
