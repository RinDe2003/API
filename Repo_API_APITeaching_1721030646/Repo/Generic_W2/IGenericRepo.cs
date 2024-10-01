using System.Linq.Expressions;

namespace Repo_API_1721030646.Repo.Generic_W2
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetListAsync();
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        int Delete(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> MaxIdAsync(Expression<Func<T, int>> expression);
        Task<int> MinIdAsync(Expression<Func<T, int>> expression);
    }
}
