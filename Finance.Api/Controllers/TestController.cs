using Finance.Application.Payments.Commands;
using Finance.Domain._Core.Response;
using Finance.Domain.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create()
        {

            var payment = new PaymentRequest();
            payment.Description = "test";
            payment.Amount = 1.34;
            payment.PaymentType = "Credit";

            var command = new PaymentCreatorCommand(payment);
            var result = await _paymentCreator.Handle(command, default);
            if (result.HasError())
            {
                return StatusCode(500, result.GetResponse());
            }

            return Created("", result.GetMessage());
        }
    }
}
