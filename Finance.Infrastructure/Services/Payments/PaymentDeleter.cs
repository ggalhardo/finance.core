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
    public class PaymentDeleter : IPaymentDeleter
    {
        private readonly ILogger<PaymentDeleter> _logger;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentDeleter(ILogger<PaymentDeleter> logger, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = paymentRepository;

        }

        public async Task<ResponseModel<string>> Delete(string id)
        {
            var response = new ResponseModel<string>();

            try { 
                _logger.LogInformation("Executing payment delete service");
                var filter = Builders<Payment>.Filter.Where(x => x.Id == Guid.Parse(id));

                var result = await _paymentRepository.Delete(filter);
                if (result == false)
                {
                    _logger.LogError("An error has occurred to delete the Payment.");
                    response.AddError("An error has occurred to delete the Payment.");
                    return response;
                }

                _logger.LogInformation($"Payment deleted successfully!");
                response.AddMessage($"Payment deleted successfully!");
            }
            catch (Exception ex) {
                _logger.LogError($"An error has occurred in payment delete. {ex.Message} {ex.StackTrace}");
                response.AddError("An error has occurred in payment delete.");
            }

            return response;
        }

    }
}
