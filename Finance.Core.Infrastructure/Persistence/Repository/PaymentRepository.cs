using Finance.Core.Infrastructure.Persistence.Context.Abstractions;
using Finance.Core.Infrastructure.Persistence.Model;
using Finance.Core.Infrastructure.Persistence.Repository.Abstractions;

namespace Finance.Core.Infrastructure.Persistence.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}
