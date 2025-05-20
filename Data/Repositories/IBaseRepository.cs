using Business.Models;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResult<bool>> AddAsync(TEntity entity);
        Task<RepositoryResult<bool>> DeleteAsync(TEntity entity);
        Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy);
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<RepositoryResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> findBy);
        Task<RepositoryResult<bool>> UpdateAsync(TEntity entity);
    }
}