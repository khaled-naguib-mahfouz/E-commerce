using System;
using System.Linq.Expressions;

namespace E_commerce.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public Repository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
