using Finance.Core.Infrastructure.Persistence.Context;
using Finance.Core.Infrastructure.Persistence.Context.Abstractions;
using HealthChecks.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Core.Infrastructure._Core.Database
{
    public static class MongoDBConfiguration
    {
        /// <summary>
        /// Extension to register MongoDB
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="configuration">The IConfigurationRoot (appsettings)</param>
        public static void AddMongoDBConfiguration(this IServiceCollection services, IConfigurationRoot configuration) {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
            services.AddSingleton<MongoDBSettings>();
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //add health-check MongoDB
            services.AddSingleton(new MongoDbHealthCheck(configuration.GetSection("MongoDB:ConnectionURI").Value, configuration.GetSection("MongoDB:DatabaseName").Value));
        }
    }
}