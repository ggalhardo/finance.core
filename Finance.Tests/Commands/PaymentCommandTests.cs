using Finance.Application.Payments.Commands;
using Finance.Application.Payments.Validators;
using Finance.Domain.Payments;
using Finance.Domain.Payments.Request;
using Finance.Infrastructure.Commands;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Finance.Tests.Commands
{
    public class PaymentQueryTests
    {
        [Fact]
        public void IsValid_CommandBase_ReturnFalse()
        {
            var command = new CommandBase();

            Assert.Throws<NotImplementedException>(() => command.IsValid());
        }

        [Fact]
        public void SetValidation_CommandBase_ReturnFalse()
        {
            try
            {
                var command = new CommandBase();
                command.SetValidation(new PaymentUpdaterValidator().Validate(new PaymentUpdaterCommand()));
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Verify_CommandBase_ReturnFalse()
        {
            try
            {
                var request = new PaymentRequest();
                request.Description = "Test";
                request.Amount = 1;
                request.Description = "Credit";
                var command = new PaymentUpdaterCommand();
                command.SetPaymentRequest(request);
                command.SetPaymentId("13231241234");

                var commandBase = new CommandBase();
                commandBase.SetValidation(new PaymentUpdaterValidator().Validate(command));
                var result = commandBase.Verify();
                Assert.True(!result.HasError());
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void IsValid_PaymentCreatorCommand_ReturnFalse()
        {
            var request = new PaymentRequest();
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1.40)]
        [InlineData(-12.40)]
        [InlineData(0)]
        public void IsValid_PaymentCreatorCommand_AmountLessThan0_ReturnFalse(double amount)
        {
            var request = new PaymentRequest();
            request.Amount = amount;
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, $"Request null not be valid with amount {amount}.");
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_PaymentCreatorCommand_WithoutPaymentType_ReturnFalse(string paymentType)
        {
            var request = new PaymentRequest();
            request.PaymentType = paymentType;
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid without payment type.");
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        public void IsValid_PaymentCreatorCommand_WithInvalidPaymentType_ReturnFalse(string paymentType)
        {
            var request = new PaymentRequest();
            request.PaymentType = paymentType;
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid with invalid payment type.");
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_PaymentCreatorCommand_WithoutDescription_ReturnFalse(string description)
        {
            var request = new PaymentRequest();
            request.Description = description;
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid without description.");
        }

        [Fact]
        public void IsValid_PaymentUpdaterCommand_ReturnFalse()
        {
            var request = new PaymentRequest();
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-1.40)]
        [InlineData(-12.40)]
        [InlineData(0)]
        public void IsValid_PaymentUpdaterCommand_AmountLessThan0_ReturnFalse(double amount)
        {
            var request = new PaymentRequest();
            request.Amount = amount;
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, $"Request null not be valid with amount {amount}.");
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_PaymentUpdaterCommand_WithoutPaymentType_ReturnFalse(string paymentType)
        {
            var request = new PaymentRequest();
            request.PaymentType = paymentType;
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid without payment type.");
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("A")]
        [InlineData("B")]
        [InlineData("C")]
        public void IsValid_PaymentUpdaterCommand_WithInvalidPaymentType_ReturnFalse(string paymentType)
        {
            var request = new PaymentRequest();
            request.PaymentType = paymentType;
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid with invalid payment type.");
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_PaymentUpdaterCommand_WithoutDescription_ReturnFalse(string description)
        {
            var request = new PaymentRequest();
            request.Description = description;
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid without description.");
        }

        [Theory]
        [InlineData(null)]
        public void IsValid_PaymentUpdaterCommand_WithoutPaymentId_ReturnFalse(string paymentId)
        {
            var request = new PaymentRequest();
            request.Description = "Test";
            request.Amount = 1;
            request.Description = "Credit";
            var command = new PaymentUpdaterCommand();
            command.SetPaymentRequest(request);
            command.SetPaymentId(paymentId);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid without description.");
        }

        [Fact]
        public void IsValid_PaymentDeleterCommand_ReturnFalse()
        {
            var command = new PaymentDeleterCommand();
            command.SetPaymentId(null);

            var result = command.IsValid();

            Assert.False(!result.Error, "Request null not be valid.");
        }

        [Fact]
        public void SetPaymentRequest_PaymentCreatorCommand_IsNotNull()
        {
            var command = new PaymentCreatorCommand();
            command.SetPaymentRequest(new PaymentRequest());

            var result = command.PaymentRequest != null;

            Assert.True(result, "Request null not be valid.");
        }

        [Fact]
        public void SetPaymentRequest_PaymentUpdaterCommand_IsNotNull()
        {
            var command = new PaymentUpdaterCommand();
            command.SetPaymentId("123456");

            var result = command.PaymentId != null;

            Assert.True(result, "PaymentId null not be valid.");
        }

        [Fact]
        public void SetPaymentRequest_PaymentDeleterCommand_IsNotNull()
        {
            var command = new PaymentDeleterCommand();
            command.SetPaymentId("123456");

            var result = command.PaymentId != null;

            Assert.True(result, "PaymentId null not be valid.");
        }
    }
}