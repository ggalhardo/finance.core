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

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}