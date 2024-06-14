using System.Linq.Expressions;

namespace JobHunt.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T?> GetLastOrDefaultOrderedBy<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> orderByExpression);

        Task<bool?> GetAnyAsync(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);
    }
}
