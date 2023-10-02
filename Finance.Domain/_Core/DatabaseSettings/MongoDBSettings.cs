namespace Finance.Domain._Core.DatabaseSettings
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; private set; }
        public string DatabaseName { get; private set; }

        public MongoDBSettings(string connectionURI, string databaseName)
        {
            this.ConnectionURI = connectionURI;
            this.DatabaseName = databaseName;
        }
    }
}