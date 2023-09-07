using Finance.Domain._Core.Response;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments.Abstractions
{
    public interface IPaymentDeleter
    {
        Task<ResponseModel<string>> Delete(string id);
    }
}
