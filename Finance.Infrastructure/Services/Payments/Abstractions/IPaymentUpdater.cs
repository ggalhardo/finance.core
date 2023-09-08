using Finance.Domain._Core.Response;
using Finance.Domain.Payments.Request;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentUpdater
    {
        Task<ResponseModel<string>> Update(string id, PaymentRequest request);
    }
}
