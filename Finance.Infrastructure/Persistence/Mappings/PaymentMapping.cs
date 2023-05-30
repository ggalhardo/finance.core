using Finance.Domain.DomainObjects;
using Finance.Domain.Payments;
using MongoDB.Bson.Serialization;

namespace Finance.Infrastructure.Persistence.Mappings
{
    public class PaymentMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Payment>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.id);
                map.MapMember(x => x.description).SetIsRequired(true);
                map.MapMember(x => x.amount).SetIsRequired(true);
            });
        }
    }
}
