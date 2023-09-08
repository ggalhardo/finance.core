using Finance.Domain.Payments;
using Finance.Infrastructure.Services.Payments.FilterBuilder.Filter;
using MongoDB.Driver;
using System;

namespace Finance.Infrastructure.Services.Payments.FilterBuilder
{
    public static class PaymentFilterBuilder
    {

        public static FilterDefinition<Payment> Build(this PaymentFilter paymentFilter)
        {
            var builder = Builders<Payment>.Filter;
            var filter = builder.Empty;

            if (paymentFilter.Id != null)
            {
                var outGuid = Guid.Empty;
                Guid.TryParse(paymentFilter.Id, out outGuid);

                filter &= builder.Eq(x => x.Id, outGuid);
            }

            return filter;
        }
    }
}
