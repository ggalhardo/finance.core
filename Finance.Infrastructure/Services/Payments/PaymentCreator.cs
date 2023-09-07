using AutoMapper;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Payments.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<ResponseModel<string>> Create(PaymentRequest request)
        {
            var response = new ResponseModel<string>();

            try 
            { 
                _logger.LogInformation("Executing payment create service");
                var payment = _mapper.Map<Payment>(request);

                var result = await _paymentRepository.Insert(payment);
                if (result == false)
                {
                    _logger.LogError("An error has occurred to insert the Payment.");
                    response.AddError("An error has occurred to insert the Payment.");
                    return response;
                }

                _logger.LogInformation($"Payment inserted successfully! PaymentId: {payment.Id}");

                response.AddResult(payment.Id.ToString());
                response.AddMessage("Payment inserted successfully!");
            }
            catch (Exception ex) {
                _logger.LogError($"An error has occurred in payment create. {ex.Message} {ex.StackTrace}");
                response.AddError("An error has occurred in payment create.");
            }

            return response;
        }

    }
}
