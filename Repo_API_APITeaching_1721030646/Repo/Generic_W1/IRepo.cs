using System.Linq.Expressions;

namespace Repo_API_1721030646.Repo.Generic_W1
{
    public interface IRepo<T> where T : class
    {
        Task<T> GetAsync(int id, bool export = true);
        Task<IEnumerable<T>> GetListAsync();
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression, bool export = true);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        int Delete(int id);
        Task<int> MaxIdAsync(int id);
        Task<int> MinIdAsync(int id);
        bool CheckExists(int id);
    }
}
