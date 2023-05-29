using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Finance.Core.Infrastructure.Persistence.Repository.Abstractions
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<bool> Insert(TEntity obj);
        Task<bool> Update(TEntity obj, FilterDefinition<TEntity> filter);
        Task<bool> Remove(FilterDefinition<TEntity> filter);
        Task<TEntity> GetOne(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter);
    }
}