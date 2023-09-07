using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentSearch
    {
        Task<ResponseModel<Payment>> SearchOne(string id);
    }
}
