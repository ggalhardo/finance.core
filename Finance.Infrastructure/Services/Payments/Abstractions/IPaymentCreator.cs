using Finance.Domain._Core.Response;
using Finance.Domain.Payments.Request;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentCreator
    {
        Task<ResponseModel<string>> Create(PaymentRequest request);
    }
}
