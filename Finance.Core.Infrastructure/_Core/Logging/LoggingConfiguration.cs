using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Finance.Core.Infrastructure._Core.Log
{
    public static class LoggingConfiguration
    {
        
        /// <summary>
        /// Extension to create new Logger configuration
        /// </summary>
        /// <param name="logger">The ILogger</param>
        /// <param name="configuration">The appsettings</param>
        /// <returns>The new Logger configuration</returns>
        public static void createLoggerElasticSearch(this ILogger logger, IConfigurationRoot configuration) 
        {
            logger = new LoggerConfiguration().WriteTo.Elasticsearch(LoggingConfiguration.createElasticOptions(configuration)).CreateLogger();
        }

        /// <summary>
        /// Create new ElasticsearchSinkOptions by appsetings
        /// </summary>
        /// <param name="configuration">The appsettings</param>
        /// <returns>The new ElasticsearchSinkOptions</returns>
        private static ElasticsearchSinkOptions createElasticOptions(IConfigurationRoot configuration) 
        {

            var IndexDefault = configuration.GetSection("AppName").Value;

            return new ElasticsearchSinkOptions(new Uri(configuration.GetSection("ElasticConfiguration:Uri").Value)) {
                AutoRegisterTemplate = true,
                IndexFormat = $"{IndexDefault}-{DateTime.UtcNow:yyyy-MM}"
            };

        }
    }
}