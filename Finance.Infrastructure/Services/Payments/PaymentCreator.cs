﻿using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments
{
    public class PaymentCreator : IPaymentCreator
    {
        private readonly ILogger<PaymentCreator> _logger;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentCreator(ILogger<PaymentCreator> logger, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = paymentRepository;

        }

        public async Task<ResponseModel<bool>> Create(PaymentRequest request)
        {
            var response = new ResponseModel<bool>();

            using (_logger.BeginScope(request))
            {
                var payment = _mapper.Map<Payment>(request);
                var result = await _paymentRepository.Insert(payment);
                if (result == false)
                {
                    _logger.LogError("An error has occurred to insert the Payment.");
                    response.AddError(true, "An error has occurred to insert the Payment.");
                }

                _logger.LogError($"Payment inserted successfully! PaymentId: {payment.Id}");

                response.AddMessage(payment.Id.ToString());
            }

            return response;
        }

    }
}
