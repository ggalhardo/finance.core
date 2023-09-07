﻿using AutoMapper;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Payments.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments
{
    public class PaymentSearch : IPaymentSearch
    {
        private readonly ILogger<PaymentSearch> _logger;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentSearch(ILogger<PaymentSearch> logger, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = paymentRepository;

        }

        public async Task<ResponseModel<Payment>> SearchOne(string id)
        {
            _logger.LogInformation("Executing payment search one service");

            var response = new ResponseModel<Payment>();
            var filter = Builders<Payment>.Filter.Where(x => x.Id == Guid.Parse(id));

            var result = await _paymentRepository.SearchOne(filter);
            if (result == null)
            {
                _logger.LogError("Payment not found.");
                response.AddError("Payment not found.");
            }

            _logger.LogInformation($"Payment found successfully!");
            response.AddMessage("Payment found successfully!");

            response.AddResult(result);

            return response;
        }

    }
}
