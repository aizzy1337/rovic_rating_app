using Microsoft.EntityFrameworkCore;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return true;
        }

        public virtual async Task<bool> Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

    }
}
