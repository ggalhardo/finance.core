using Amazon.Runtime.Internal.Util;
using Finance.Application.Payments.Commands;
using Finance.Core.Logging;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace Finance.Api.Controllers
{

    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly LoggingTracking _loggingTracking;
        private readonly IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> _paymentCreator;

        public TestController(ILogger<TestController> logger, LoggingTracking loggingTracking, IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> paymentCreator) {
            _logger = logger;
            _paymentCreator = paymentCreator;
            _loggingTracking = loggingTracking;
        }

        [HttpPost]
        [Route("payments/create")]
        public async Task<IActionResult> Create()
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation("Executing endpoint payments/create");
                var payment = new PaymentRequest();
                payment.Description = "test";
                payment.Amount = 1.34;
                payment.PaymentType = "Credit";

                var command = new PaymentCreatorCommand(payment);
                var result = await _paymentCreator.Handle(command, default);
                if (result.HasError())
                {
                    return StatusCode(500, result.GetResponse());
                }
                return Created("", result.GetMessage());
            }
        }
    }
}
