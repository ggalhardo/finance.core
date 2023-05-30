using Finance.Core.Infrastructure.Persistence.Model;
using MongoDB.Bson.Serialization;

namespace Finance.Core.Infrastructure.Persistence.Map
{
    public class PaymentMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Payment>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.id);
                map.MapMember(x => x.description).SetIsRequired(true);
                map.MapMember(x => x.value).SetIsRequired(true);
            });
        }
    }
}
