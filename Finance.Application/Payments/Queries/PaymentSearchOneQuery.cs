using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using MediatR;

namespace Finance.Application.Payments.Queries
{
    public class PaymentSearchOneQuery : IRequest<ResponseModel<Payment>>
    {
        public string PaymentId { get; private set; }

        public void SetPaymentId(string paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}
