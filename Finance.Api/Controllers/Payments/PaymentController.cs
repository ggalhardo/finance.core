using Finance.Application.Payments.Commands;
using Finance.Application.Payments.Queries;
using Finance.Core.Logging;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Domain.Payments.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace Finance.Api.Payments.Controllers
{

    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        private readonly LoggingTracking _loggingTracking;
        private readonly IRequestHandler<PaymentCreatorCommand, ResponseModel<string>> _paymentCreator;
        private readonly IRequestHandler<PaymentUpdaterCommand, ResponseModel<string>> _paymentUpdater;
        private readonly IRequestHandler<PaymentDeleterCommand, ResponseModel<string>> _paymentDeleter;
        private readonly IRequestHandler<PaymentSearchOneQuery, ResponseModel<Payment>> _paymentSearchOne;

        public PaymentController(ILogger<PaymentController> logger, 
                                 LoggingTracking loggingTracking, 
                                 IRequestHandler<PaymentCreatorCommand, ResponseModel<string>> paymentCreator,
                                 IRequestHandler<PaymentUpdaterCommand, ResponseModel<string>> paymentUpdater,
                                 IRequestHandler<PaymentDeleterCommand, ResponseModel<string>> paymentDeleter,
                                 IRequestHandler<PaymentSearchOneQuery, ResponseModel<Payment>> paymentSearchOne) 
        {
            _logger = logger;
            _paymentCreator = paymentCreator;
            _paymentUpdater = paymentUpdater;
            _paymentDeleter = paymentDeleter;
            _loggingTracking = loggingTracking;
            _paymentSearchOne = paymentSearchOne;
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

                if (result.HasError()) return StatusCode(500, result);

                return Created("", result.GetResponse());
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

                return Ok();
            }
        }

        [HttpDelete]
        [Route("payment/{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation($"Executing endpoint payment/{id}/delete");

                var command = new PaymentDeleterCommand();
                command.SetPaymentId(id);
                var result = await _paymentDeleter.Handle(command, default);

                if (result.HasError()) return StatusCode(500, result.GetResponse());

                return NoContent();
            }
        }

        [HttpGet]
        [Route("payment/{id}/search")]
        public async Task<IActionResult> SearchOne([FromRoute]string id)
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation($"Executing endpoint payment/{id}/search");

                var command = new PaymentSearchOneQuery();
                command.SetPaymentId(id);
                var result = await _paymentSearchOne.Handle(command, default);

                if (result.Result == null) return NotFound(result);

                if (result.HasError()) return StatusCode(500, result.GetResponse());

                return Ok(result);
            }
        }
    }
}
