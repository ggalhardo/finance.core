using Finance.Application.Payments.Commands;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Application.Payments.Handlers
{
    public class PaymentUpdaterHandler : IRequestHandler<PaymentUpdaterCommand, ResponseModel<bool>>
    {
        private readonly ILogger<PaymentUpdaterHandler> _logger;
        private readonly IPaymentUpdater _paymentUpdater;

        public PaymentUpdaterHandler(ILogger<PaymentUpdaterHandler> logger, IPaymentUpdater paymentUpdater)
        {
            _logger = logger;
            _paymentUpdater = paymentUpdater;
        }

        public async Task<ResponseModel<bool>> Handle(PaymentUpdaterCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing PaymentUpdaterHandler");
            _logger.LogInformation("Validating PaymentUpdaterCommand");
            var resultValidation = command.IsValid();
            if (resultValidation.Error)
            {
                _logger.LogError($"PaymentUpdaterCommand is invalid. {resultValidation.GetMessage()}");
                return resultValidation;
            }

            _logger.LogInformation("PaymentUpdaterCommand is valid.");
            return await _paymentUpdater.Update(command.PaymentId, command.PaymentRequest);
        }
    }
}
