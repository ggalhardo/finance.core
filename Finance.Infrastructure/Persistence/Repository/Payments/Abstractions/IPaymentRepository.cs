using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Base.Abstractions;

namespace Finance.Infrastructure.Persistence.Repository.Payments.Abstractions
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
    }
}
