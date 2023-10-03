using Finance.Core.Logging.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Finance.Core.Logging.Middleware
{

    public class LoggingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("CorrelationId", context.GetCorrelationId());

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occurred! \n Exception: {ex.Message} \n Trace: {ex.StackTrace}");
            }
        }
    }
}
