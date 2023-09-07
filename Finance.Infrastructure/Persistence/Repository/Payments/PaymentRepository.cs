using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Context.Abstractions;
using Finance.Infrastructure.Persistence.Repository.Base;
using Finance.Infrastructure.Persistence.Repository.Payments.Abstractions;

namespace Finance.Infrastructure.Persistence.Repository.Payments
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}
