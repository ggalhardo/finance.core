using Finance.Application.Payments.Commands;
using Finance.Core.Logging;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Finance.Api.Payments.Controllers
{

    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        private readonly LoggingTracking _loggingTracking;
        private readonly IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> _paymentCreator;
        private readonly IRequestHandler<PaymentUpdaterCommand, ResponseModel<bool>> _paymentUpdater;

        public PaymentController(ILogger<PaymentController> logger, 
                                 LoggingTracking loggingTracking, 
                                 IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> paymentCreator,
                                 IRequestHandler<PaymentUpdaterCommand, ResponseModel<bool>> paymentUpdater) 
        {
            _logger = logger;
            _paymentCreator = paymentCreator;
            _paymentUpdater = paymentUpdater;
            _loggingTracking = loggingTracking;
        }

        [HttpPost]
        [Route("payment/create")]
        public async Task<IActionResult> Create(PaymentRequest request)
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation("Executing endpoint payment/create");

                var command = new PaymentCreatorCommand();
                command.SetPaymentRequest(request);
                var result = await _paymentCreator.Handle(command, default);

                if (result.HasError()) return StatusCode(500, result.GetResponse());

                return Created("", result.GetMessage());
            }
        }

        [HttpPut]
        [Route("payment/{id}/update")]
        public async Task<IActionResult> Update([FromRoute]string id, PaymentRequest request)
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation($"Executing endpoint payment/{id}/update");

                var command = new PaymentUpdaterCommand();
                command.SetPaymentRequest(request);
                command.SetPaymentId(id);
                var result = await _paymentUpdater.Handle(command, default);

                if (result.HasError()) return StatusCode(500, result.GetResponse());

                return Created("", result.GetMessage());
            }
        }
    }
}
