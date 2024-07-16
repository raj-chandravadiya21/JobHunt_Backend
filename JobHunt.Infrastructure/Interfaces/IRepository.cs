using System.Linq.Expressions;

namespace JobHunt.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T?> GetFirstOrDefaultNullable(Expression<Func<T, bool>> predicate);

        Task<T?> GetLastOrDefaultOrderedBy<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> orderByExpression);

        Task<bool?> GetAnyAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);

        void UpdateAsync(T entity);

        Task AddRangeAsync(List<T> entity);

        Task<List<T>> WhereList(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAllAsync();

        IQueryable<T?> GetWhere(Expression<Func<T, bool>> predicate);

        void RemoveRange(List<T> entities);

        void Remove(T entity);

        Task<int> ConditionalCount(Expression<Func<T, bool>> predicate);
    }
}
