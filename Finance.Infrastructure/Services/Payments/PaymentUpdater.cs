using AutoMapper;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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

        public async Task<ResponseModel<bool>> Update(string id, PaymentRequest request)
        {
            _logger.LogInformation("Executing payment update service");

            var response = new ResponseModel<bool>();
            var payment = _mapper.Map<Payment>(request);
            var filter = Builders<Payment>.Filter.Where(x => x.Id == Guid.Parse(id));

            var result = await _paymentRepository.Update(payment, filter);
            if (result == false)
            {
                _logger.LogError("An error has occurred to update the Payment.");
                response.AddError(true, "An error has occurred to update the Payment.");
            }

            _logger.LogInformation($"Payment updated successfully!");

            response.AddMessage(payment.Id.ToString());

            return response;
        }

    }
}
