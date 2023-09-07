using Finance.Application.Payments.Commands;
using Finance.Domain._Core.Response;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Application.Payments.Handlers
{
    public class PaymentDeleterHandler : IRequestHandler<PaymentDeleterCommand, ResponseModel<string>>
    {
        private readonly ILogger<PaymentDeleterHandler> _logger;
        private readonly IPaymentDeleter _paymentDeleter;

        public PaymentDeleterHandler(ILogger<PaymentDeleterHandler> logger, IPaymentDeleter paymentDeleter)
        {
            _logger = logger;
            _paymentDeleter = paymentDeleter;
        }

        public async Task<ResponseModel<string>> Handle(PaymentDeleterCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing PaymentDeleterHandler");
            return await _paymentDeleter.Delete(command.PaymentId);
        }
    }
}
