using Finance.Domain._Core.DatabaseSettings;
using Finance.Infrastructure.Persistence.Context;
using Finance.Infrastructure.Persistence.Context.Abstractions;
using HealthChecks.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Core.Database
{
    public static class MongoDBConfiguration
    {
        /// <summary>
        /// Extension to register MongoDB
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        /// <param name="configuration">The IConfigurationRoot (appsettings)</param>
        public static void AddMongoDBConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var mongoDBSettings = configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            services.AddSingleton(mongoDBSettings);
            services.AddScoped<IDatabaseContext, DatabaseContext>();

            //add health-check MongoDB
            services.AddSingleton(new MongoDbHealthCheck(mongoDBSettings.ConnectionURI, mongoDBSettings.DatabaseName));
        }
    }
}