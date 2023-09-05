﻿using Amazon.Runtime.Internal.Util;
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

namespace Finance.Api.Controllers
{

    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly LoggingTracking _loggingTracking;
        private readonly IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> _paymentCreator;
        private readonly IRequestHandler<PaymentUpdaterCommand, ResponseModel<bool>> _paymentUpdater;

        public TestController(ILogger<TestController> logger,
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
        [Route("test/payment/create")]
        public async Task<IActionResult> TestCreate()
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation("Executing endpoint payment/create");
                var payment = new PaymentRequest();
                payment.Description = "test";
                payment.Amount = 1.34;
                payment.PaymentType = "Credit";

                var command = new PaymentCreatorCommand();
                command.SetPaymentRequest(payment);
                var result = await _paymentCreator.Handle(command, default);
                if (result.HasError())
                {
                    return StatusCode(500, result.GetResponse());
                }
                return Created("", result.GetMessage());
            }
        }

        [HttpPut]
        [Route("test/payment/{id}/update")]
        public async Task<IActionResult> TestUpdate([FromRoute]string id)
        {
            using (LogContext.PushProperty("TrackingId", _loggingTracking.TrackingId))
            {
                _logger.LogInformation($"Executing endpoint payment/{id}/update");
                var payment = new PaymentRequest();
                payment.Description = "test2";
                payment.Amount = 2.98;
                payment.PaymentType = "Debit";

                var command = new PaymentUpdaterCommand();
                command.SetPaymentRequest(payment);
                command.SetPaymentId(id);
                var result = await _paymentUpdater.Handle(command, default);

                if (result.HasError()) return StatusCode(500, result.GetResponse());

                return Created("", result.GetMessage());
            }
        }
    }
}
