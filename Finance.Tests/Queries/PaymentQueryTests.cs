using Finance.Application.Payments.Queries;
using Xunit;

namespace Finance.Tests.Queries
{
    public class PaymentQueryTests
    {

        [Fact]
        public void SetPaymentRequest_PaymentSearchOneQuery_IsNotNull()
        {
            var command = new PaymentSearchOneQuery();
            command.SetPaymentId("123456");

            var result = command.PaymentId != null;

            Assert.True(result, "PaymentId null not be valid.");
        }
    }
}