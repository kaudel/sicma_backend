using Sicma.Entities;
using System.Linq.Expressions;

namespace Sicma.Repositorys.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector);
        Task<(ICollection<TResult> Collection, int TotalRecords)> GetAllAsync<TResult, TKey>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int page = 1, int rows = 5);
        Task<TEntity?> FindByIdAsync(string id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync();
        Task DeleteAsync(string id);
    }
}
