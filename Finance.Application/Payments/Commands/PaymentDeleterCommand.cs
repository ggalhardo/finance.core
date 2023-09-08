using Finance.Application.Payments.Validators;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Commands;

namespace Finance.Application.Payments.Commands
{
    public class PaymentDeleterCommand : CommandBase
    {
        public string PaymentId { get; private set; }

        public void SetPaymentId(string paymentId)
        {
            this.PaymentId = paymentId;
        }

        public override ResponseModel<string> IsValid()
        {
            base.SetValidation(new PaymentDeleterValidator().Validate(this));
            return base.Verify();
        }
    }
}