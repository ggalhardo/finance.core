using Finance.Application.Payments.Validators;
using Finance.Domain.DomainObjects;
using Finance.Domain.Payments;
using Finance.Infrastructure.Commands;

namespace Finance.Application.Payments.Commands
{
    public class PaymentCreatorCommand : CommandBase
    {
        public PaymentRequest _paymentRequest { get; private set; }

        public PaymentCreatorCommand(PaymentRequest paymentRequest)
        {
            _paymentRequest = paymentRequest;
        }

        public override ResponseModel<bool> IsValid()
        {
            _validationResult = new PaymentCreatorValidator().Validate(this);
            return base.Verify();
        }
    }
}
