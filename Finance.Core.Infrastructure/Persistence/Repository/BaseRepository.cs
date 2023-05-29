using Finance.Core.Infrastructure.Persistence.Context.Abstractions;
using Finance.Core.Infrastructure.Persistence.Repository.Abstractions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finance.Core.Infrastructure.Persistence.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IDatabaseContext _context;
        protected IMongoCollection<TEntity> _dbSet;

        protected BaseRepository(IDatabaseContext context)
        {
            _context = context;

            _dbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<bool> Insert(TEntity obj)
        {
            _context.AddCommand(() => _dbSet.InsertOneAsync(obj));
            return await _context.SaveChanges();
        }

        public virtual async Task<bool> Update(TEntity obj, FilterDefinition<TEntity> filter)
        {
            _context.AddCommand(() => _dbSet.ReplaceOneAsync(filter, obj));
            return await _context.SaveChanges();
        }

        public virtual async Task<bool> Remove(FilterDefinition<TEntity> filter)
        {
            _context.AddCommand(() => _dbSet.DeleteOneAsync(filter));
            return await _context.SaveChanges();
        }

        public virtual async Task<TEntity> GetOne(FilterDefinition<TEntity> filter)
        {
            var result = await _dbSet.FindAsync(filter);
            return result.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter)
        {
            var results = await _dbSet.FindAsync(filter);
            return results.ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}