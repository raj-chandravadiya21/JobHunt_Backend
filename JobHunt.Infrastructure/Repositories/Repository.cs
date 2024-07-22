using JobHunt.Domain.Entities;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobHunt.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DefaultdbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DefaultdbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await _dbSet.FirstOrDefaultAsync(predicate);
            var typeName = typeof(T).Name;
            var message = $"No {typeName} found matching the criteria";
            if (data == null)
                throw new NullObjectException(message);
            return data;
        }

        public async Task<T?> GetFirstOrDefaultNullable(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T? data = _dbSet.FirstOrDefault(predicate);
            return Task.FromResult(data);
        }

        public async Task<T?> GetLastOrDefaultOrderedBy<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> orderByExpression)
        {
            var data = await _dbSet.Where(predicate)
                       .OrderBy(orderByExpression)
                       .LastOrDefaultAsync();

            var typeName = typeof(T).Name;
            var message = $"No {typeName} found matching the criteria";
            if (data == null)
                throw new NullObjectException(message);
            return data;
        }

        public async Task<bool?> GetAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> WhereList(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IQueryable<T?> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void RemoveRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> ConditionalCount(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}