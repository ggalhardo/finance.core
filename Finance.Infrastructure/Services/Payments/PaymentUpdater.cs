using AutoMapper;
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
    public class PaymentUpdater : IPaymentUpdater
    {
        private readonly ILogger<PaymentUpdater> _logger;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentUpdater(ILogger<PaymentUpdater> logger, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = paymentRepository;

        }

        public async Task<ResponseModel<string>> Update(string id, PaymentRequest request)
        {
            var response = new ResponseModel<string>();

            try { 
                _logger.LogInformation("Executing payment update service");

                var payment = _mapper.Map<Payment>(request);
                var filter = Builders<Payment>.Filter.Where(x => x.Id == Guid.Parse(id));

                var result = await _paymentRepository.Update(payment, filter);
                if (result == false)
                {
                    _logger.LogError("An error has occurred to update the Payment.");
                    response.AddError("An error has occurred to update the Payment.");
                    return response;
                }

                _logger.LogInformation($"Payment updated successfully!");
                response.AddMessage("Payment updated successfully!");

                response.AddResult(payment.Id.ToString());
            }
            catch (Exception ex) {
                _logger.LogError($"An error has occurred in payment update. {ex.Message} {ex.StackTrace}");
                response.AddError("An error has occurred in payment update.");
            }

            return response;
        }

    }
}
