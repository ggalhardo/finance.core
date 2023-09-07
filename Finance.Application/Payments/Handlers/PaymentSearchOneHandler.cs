using Finance.Application.Payments.Queries;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using Finance.Infrastructure.Services.Payments.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Application.Payments.Handlers
{
    public class PaymentSearchOneHandler : IRequestHandler<PaymentSearchOneQuery, ResponseModel<Payment>>
    {
        private readonly ILogger<PaymentSearchOneHandler> _logger;
        private readonly IPaymentSearch _paymentSearch;

        public PaymentSearchOneHandler(ILogger<PaymentSearchOneHandler> logger, IPaymentSearch paymentSearch)
        {
            _logger = logger;
            _paymentSearch = paymentSearch;
        }

        public async Task<ResponseModel<Payment>> Handle(PaymentSearchOneQuery command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Executing PaymentSearchOneHandler");

            return await _paymentSearch.SearchOne(command.PaymentId);
        }
    }
}
