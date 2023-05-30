using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Finance.Infrastructure.Persistence.Context.Abstractions
{
    public interface IDatabaseContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<bool> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}