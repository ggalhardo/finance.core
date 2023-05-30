using Finance.Infrastructure.Persistence.Mappings;
using MongoDB.Bson.Serialization.Conventions;

namespace Finance.Infrastructure.Persistence
{
    public static class DatabasePersistence
    {
        public static void Configure()
        {
            //Mappings
            PaymentMapping.Configure();

            //Conventions
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };
            ConventionRegistry.Register("Finance.Core Conventions", pack, t => true);
        }
    }
}
