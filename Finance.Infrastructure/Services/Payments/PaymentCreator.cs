using AutoMapper;
using Finance.Domain.DomainObjects;
using Finance.Domain.Payments;
using Finance.Infrastructure.Persistence.Repository.Abstractions;
using Finance.Infrastructure.Services.Payments.Abstractions;
using System;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Payments
{
    public class PaymentCreator : IPaymentCreator
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper mapper;

        public PaymentCreator(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

        }

        public async Task<ResponseModel<bool>> Create(PaymentRequest request)
        {
            var response = new ResponseModel<bool>();

            var payment = mapper.Map<Payment>(request);
            var result = await _paymentRepository.Insert(payment);
            if (result == false)
            {
                response.AddError(true, "An error has occurred to insert the Payment.");
            }

            return response;
        }

    }
}
