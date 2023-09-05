using Finance.Application.Payments.Commands;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Application.Payments.Handlers
{
    public class PaymentCreatorHandler : IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>>
    {
        private readonly ILogger<PaymentCreatorHandler> _logger;
        private readonly IPaymentCreator _paymentCreator;

        public PaymentCreatorHandler(ILogger<PaymentCreatorHandler> logger, IPaymentCreator paymentCreator)
        {
            _logger = logger;
            _paymentCreator = paymentCreator;
        }

        public async Task<ResponseModel<bool>> Handle(PaymentCreatorCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing PaymentCreatorHandler");
            _logger.LogInformation("Validating PaymentCreatorCommand");
            var resultValidation = command.IsValid();
            if (resultValidation.Error)
            {
                _logger.LogError($"PaymentCreatorCommand is invalid. {resultValidation.GetMessage()}");
                return resultValidation;
            }

            _logger.LogInformation("PaymentCreatorCommand is valid.");
            return await _paymentCreator.Create(command.PaymentRequest);
        }
    }
}
