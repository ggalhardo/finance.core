﻿using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Context.Abstractions;
using Finance.Infrastructure.Persistence.Repository.Abstractions;

namespace Finance.Infrastructure.Persistence.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IDatabaseContext context) : base(context)
        {
        }
    }
}
