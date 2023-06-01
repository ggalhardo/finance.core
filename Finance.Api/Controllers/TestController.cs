using Finance.Application.Payments.Commands;
using Finance.Domain.DomainObjects;
using Finance.Domain.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Finance.Api.Controllers
{

    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> _paymentCreator;

        public TestController(IRequestHandler<PaymentCreatorCommand, ResponseModel<bool>> paymentCreator) {
            _paymentCreator = paymentCreator;
        }

        [HttpPost]
        [Route("payments/create")]
        public async Task<IActionResult> create()
        {

            var payment = new PaymentRequest();
            payment.description = "test";
            payment.amount = 1;

            var command = new PaymentCreatorCommand(payment);
            var result = await _paymentCreator.Handle(command, default);
            if (result.HasError())
            {
                return StatusCode(500, result.GetErrorMessage());
            }

            return Created("",null);
        }
    }
}
