using System;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace Finance.Core.Infrastructure.Log
{
    public class LoggingConfiguration
    {
         public static ElasticsearchSinkOptions createElasticOptions(IConfigurationRoot configuration) 
         {

            var IndexDefault = configuration.GetSection("AppName").Value;

            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"])) {
                AutoRegisterTemplate = true,
                IndexFormat = $"{IndexDefault}-{DateTime.UtcNow:yyyy-MM}"
            };

        }
    }
}