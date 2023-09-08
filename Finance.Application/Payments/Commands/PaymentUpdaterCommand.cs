using Finance.Application.Payments.Validators;
using Finance.Domain._Core.Response;

namespace Finance.Application.Payments.Commands
{
    public class PaymentUpdaterCommand : PaymentCreatorCommand
    {
        public string PaymentId { get; private set; }

        public void SetPaymentId(string paymentId)
        {
            this.PaymentId = paymentId;
        }

        public override ResponseModel<string> IsValid()
        {
            var creatorValidator = new PaymentCreatorValidator().Validate(this);
            if (!creatorValidator.IsValid)
            {
                base.SetValidation(creatorValidator);
                return base.Verify();
            }

            base.SetValidation(new PaymentUpdaterValidator().Validate(this));
            return base.Verify();
        }
    }
}
