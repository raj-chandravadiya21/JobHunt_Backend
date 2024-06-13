using System.Linq.Expressions;

namespace JobHunt.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> predicate);

        Task CreateAsync(T entity);
    }
}
