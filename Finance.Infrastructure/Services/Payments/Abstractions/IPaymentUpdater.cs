using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentUpdater
    {
        Task<ResponseModel<bool>> Update(string id, PaymentRequest request);
    }
}
