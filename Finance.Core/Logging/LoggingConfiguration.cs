using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace Finance.Core.Logging
{
    public static class LoggingConfiguration
    {

        /// <summary>
        /// Extension to create new Logger configuration
        /// </summary>
        /// <param name="logger">The ILogger</param>
        /// <param name="configuration">The appsettings</param>
        /// <returns>The new Logger configuration</returns>
        public static ILogger CreateLogger(IConfigurationRoot configuration, string environment)
        {
            /*if (environment.ToLower().StartsWith("dev"))
            {
                return new LoggerConfiguration().WriteTo.Console(new JsonFormatter())
                                            .Enrich.FromLogContext()
                                            .Enrich.WithEnvironmentName()
                                            .CreateLogger();
            }*/

            return new LoggerConfiguration().WriteTo.Elasticsearch(CreateElasticOptions(configuration))
                                            .Enrich.FromLogContext()
                                            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                            .CreateLogger();
        }

        /// <summary>
        /// Create new ElasticsearchSinkOptions by appsetings
        /// </summary>
        /// <param name="configuration">The appsettings</param>
        /// <returns>The new ElasticsearchSinkOptions</returns>
        private static ElasticsearchSinkOptions CreateElasticOptions(IConfigurationRoot configuration)
        {

            var IndexDefault = configuration.GetSection("AppName").Value;

            return new ElasticsearchSinkOptions(new Uri(Environment.GetEnvironmentVariable("ELASTIC_CONFIGURATION_URI")))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{IndexDefault}-{DateTime.UtcNow:yyyy-MM}"
            };

        }
    }
}