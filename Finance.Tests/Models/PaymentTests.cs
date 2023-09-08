using Finance.Domain.DomainObjects;
using Finance.Domain.DomainObjects.Extensions;
using Finance.Domain.Payments;
using Finance.Domain.Payments.Request;
using Xunit;

namespace Finance.Tests.Models
{
    public class PaymentTests
    {

        [Fact]
        public void Id_Payment()
        {
            
            var Payment = new Payment(Guid.Empty, "test", 1.5, new PaymentType(1));
            Assert.Equal(Guid.Empty, Payment.Id);
        }

        [Fact]
        public void Description_Payment()
        {
            
            var Payment = new Payment(Guid.Empty, "test", 1.5, new PaymentType(1));
            Assert.Equal("test", Payment.Description);
        }

        [Fact]
        public void Amount_Payment()
        {
            
            var Payment = new Payment(Guid.Empty, "test", 1.5, new PaymentType(1));
            Assert.Equal(1.5, Payment.Amount);
        }

        [Fact]
        public void Type_Payment()
        {
            
            var Payment = new Payment(Guid.Empty, "test", 1.5, new PaymentType(1));
            Assert.Equal(new PaymentType(1).Id, Payment.Type.Id);
        }

        [Fact]
        public void Constructor1_Payment()
        {
            try
            {
                var Payment = new Payment("test", 1.5, 1);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Constructor2_Payment()
        {
            try
            {
                var Payment = new Payment(Guid.Empty, "test", 1.5, 1);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Constructor3_Payment()
        {
            try
            {
                var Payment = new Payment(Guid.Empty, "test", 1.5, new PaymentType(1));
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Constructor_PaymentType()
        {
            try
            {
                var PaymentType = new PaymentType(1);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Id_PaymentType()
        {
            var PaymentType = new PaymentType(1);
            Assert.Equal(1, PaymentType.Id);
        }

        [Fact]
        public void Amount_PaymentRequest()
        {
            var PaymentRequest = new PaymentRequest();
            PaymentRequest.Amount = 1;
            Assert.Equal(1, PaymentRequest.Amount);
        }

        [Fact]
        public void PaymentType_PaymentRequest()
        {
            var PaymentRequest = new PaymentRequest();
            PaymentRequest.PaymentType = "Credit";
            Assert.Equal("Credit", PaymentRequest.PaymentType);
        }

        [Fact]
        public void Description_PaymentRequest()
        {
            var PaymentRequest = new PaymentRequest();
            PaymentRequest.Description = "Test";
            Assert.Equal("Test", PaymentRequest.Description);
        }
        

        [Fact]
        public void Map_PaymentTypeExtension()
        {
            var paymentType = 1;
            var paymentTypeMap = paymentType.Map();
            Assert.Equal(new PaymentType(1).Id, paymentTypeMap.Id);
        }

    }
}