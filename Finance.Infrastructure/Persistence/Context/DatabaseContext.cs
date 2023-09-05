using Finance.Domain._Core.DatabaseSettings;
using Finance.Infrastructure.Persistence.Context.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Persistence.Context
{
    public class DatabaseContext : IDatabaseContext
    {
        private ILogger<DatabaseContext> _logger { get; set; }
        private IMongoDatabase _database { get; set; }
        public IClientSessionHandle _session { get; set; }
        public MongoClient _mongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly MongoDBSettings _mongoDBSettings;

        public DatabaseContext(ILogger<DatabaseContext> logger, MongoDBSettings mongoDBSettings)
        {
            _logger = logger;
            _mongoDBSettings = mongoDBSettings;
            _commands = new List<Func<Task>>();
        }

        public async Task<bool> SaveChanges()
        {
            ConfigureMongo();

            //_session = await _mongoClient.StartSessionAsync();

            try
            {
                //_session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                //await _session.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An exception as ocurred in SaveChanges. Ex: {ex.Message}");
                //await _session.AbortTransactionAsync();
                return false;
            }
        }

        private void ConfigureMongo()
        {
            try
            {
                if (_mongoClient != null)
                {
                    return;
                }

                _mongoClient = new MongoClient(_mongoDBSettings.ConnectionURI);
                _database = _mongoClient.GetDatabase(_mongoDBSettings.DatabaseName);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An exception as ocurred in ConfigureMongo. Ex: {ex.Message}");
                Dispose();
            }
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return _database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

    }
}