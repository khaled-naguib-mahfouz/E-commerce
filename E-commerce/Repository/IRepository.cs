using System.Linq.Expressions;

namespace E_commerce.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T> FindAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

    }
}
