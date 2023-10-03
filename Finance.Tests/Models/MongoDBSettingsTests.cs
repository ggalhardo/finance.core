using Finance.Domain._Core.DatabaseSettings;
using Xunit;

namespace Finance.Tests.Models
{
    public class MongoDBSettingsTests
    {

        [Fact]
        public void ConnectionURI_MongoDBSettings()
        {
            var MongoDBSettings = new MongoDBSettings("Test", "Test");
            Assert.Equal("Test", MongoDBSettings.ConnectionURI);
        }

        [Fact]
        public void DatabaseName_MongoDBSettings()
        {
            var MongoDBSettings = new MongoDBSettings("Test", "Test");
            Assert.Equal("Test", MongoDBSettings.DatabaseName);
        }

    }
}