using Microsoft.EntityFrameworkCore;
using Repo_API_1721030646.Data;
using System.Linq.Expressions;

namespace Repo_API_1721030646.Repo.Generic_W2
{
    public class GenericAPITeachingRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApiteachingContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericAPITeachingRepo(ApiteachingContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public int Delete(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                var result = _context.SaveChanges();
                return result;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> MaxIdAsync(Expression<Func<T, int>> expression)
        {
            return await _dbSet.MaxAsync(expression);
        }
        public async Task<int> MinIdAsync(Expression<Func<T, int>> expression)
        {
            return await _dbSet.MinAsync(expression);
        }
    }
}
