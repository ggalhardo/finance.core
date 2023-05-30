using Finance.Core.Infrastructure.Persistence.Map;
using MongoDB.Bson.Serialization.Conventions;

namespace Finance.Core.Infrastructure.Persistence
{
    public static class DatabasePersistence
    {
        public static void Configure()
        {
            PaymentMap.Configure();

            // Conventions
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("Finance.Core Conventions", pack, t => true);
        }
    }
}
