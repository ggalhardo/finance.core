using Finance.Domain._Core.Response;
using Xunit;

namespace Finance.Tests.Models
{
    public class ResponseModelTests
    {

        [Fact]
        public void Constructor_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddError_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                responseModel.AddError(string.Empty);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddResult_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                responseModel.AddResult(string.Empty);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void AddMessage_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                responseModel.AddMessage(string.Empty);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void GetMessage_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                var result = responseModel.GetMessage();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void HasError_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                var result = responseModel.HasError();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void HasResult_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                var result = responseModel.HasResult();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void GetResponse_ResponseModel()
        {
            try
            {
                var responseModel = new ResponseModel<string>();
                var result = responseModel.GetResponse();
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

    }
}