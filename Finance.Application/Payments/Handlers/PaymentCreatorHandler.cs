using Finance.Application.Payments.Commands;
using Finance.Domain.DomainObjects;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Application.Payments.Handlers
{
    public class PaymentCreatorHandler : IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>>
    {
        private readonly IPaymentCreator _paymentCreator;

        public PaymentCreatorHandler(IPaymentCreator paymentCreator)
        {
            _paymentCreator = paymentCreator;
        }

        public async Task<ResponseModel<bool>> Handle(PaymentCreatorCommand command, CancellationToken cancellationToken)
        {
            var resultValidation = command.IsValid();
            if (resultValidation.error)
            {
                return resultValidation;
            }

            var resultCreator = await _paymentCreator.Create(command._paymentRequest);

            return resultCreator;
        }
    }
}
