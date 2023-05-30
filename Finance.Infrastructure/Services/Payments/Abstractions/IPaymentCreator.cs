using Finance.Domain.DomainObjects;
using Finance.Domain.Payments;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentCreator
    {
        Task<ResponseModel<bool>> Create(PaymentRequest request);
    }
}
