using Finance.Domain._Core.DatabaseSettings;
using Finance.Domain.DomainObjects;
using Finance.Domain.DomainObjects.Extensions;
using Xunit;

namespace Finance.Tests.Models
{
    public class MongoDBSettingsTests
    {

        [Fact]
        public void ConnectionURI_MongoDBSettings()
        {
            var MongoDBSettings = new MongoDBSettings();
            MongoDBSettings.ConnectionURI = "Test";
            Assert.Equal("Test", MongoDBSettings.ConnectionURI);
        }

        [Fact]
        public void DatabaseName_MongoDBSettings()
        {
            var MongoDBSettings = new MongoDBSettings();
            MongoDBSettings.DatabaseName = "Test";
            Assert.Equal("Test", MongoDBSettings.DatabaseName);
        }

    }
}