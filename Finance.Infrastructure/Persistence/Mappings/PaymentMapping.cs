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
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Description).SetIsRequired(true);
                map.MapMember(x => x.Amount).SetIsRequired(true);
                map.MapMember(x => x.Type).SetIsRequired(true);
            });
        }
    }
}
