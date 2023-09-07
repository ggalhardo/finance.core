using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Finance.Infrastructure.Persistence.Repository.Base.Abstractions
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<bool> Insert(TEntity obj);
        Task<bool> Update(TEntity obj, FilterDefinition<TEntity> filter);
        Task<bool> Delete(FilterDefinition<TEntity> filter);
        Task<TEntity> SearchOne(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> SearchAll(FilterDefinition<TEntity> filter);
    }
}