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
    }
}