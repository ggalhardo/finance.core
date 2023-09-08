using AutoMapper;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Payments.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using Finance.Infrastructure.Services.Payments.FilterBuilder;
using Finance.Infrastructure.Services.Payments.FilterBuilder.Filter;
using Microsoft.Extensions.Logging;
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
            var response = new ResponseModel<Payment>();

            try
            {

                _logger.LogInformation("Executing payment search one service");

                var filter = new PaymentFilter().WithId(id)
                                                .Build();

                var result = await _paymentRepository.SearchOne(filter);
                if (result == null)
                {
                    _logger.LogError("Payment not found.");
                    response.AddError("Payment not found.");
                    return response;
                }

                _logger.LogInformation($"Payment found successfully!");
                response.AddMessage("Payment found successfully!");

                response.AddResult(result);
            }
            catch (Exception ex) {
                _logger.LogError($"An error has occurred in payment search. {ex.Message} {ex.StackTrace}");
                response.AddError("An error has occurred in payment search.");
            }

            return response;
        }

    }
}
