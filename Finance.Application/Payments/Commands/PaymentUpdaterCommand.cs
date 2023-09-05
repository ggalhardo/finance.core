using System;

namespace Finance.Application.Payments.Commands
{
    public class PaymentUpdaterCommand : PaymentCreatorCommand
    {
        public string PaymentId { get; private set; }

        public void SetPaymentId(string paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}
