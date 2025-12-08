using Microsoft.EntityFrameworkCore;
using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Entities.Interfaces;
using Sicma.Repositorys.Interfaces;
using System.Linq.Expressions;

namespace Sicma.Repositorys.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly DbSicmaContext dbSicmaContext;

        public BaseRepository(DbSicmaContext context)
        {
            dbSicmaContext = context;
        }

        public async Task< ICollection<TEntity>> GetAllAsync()
        {
            var result = await dbSicmaContext.Set<TEntity>()
                                .Where(p => p.IsActive)
                                .AsNoTracking()
                                .ToListAsync();
            return result;
        }

        public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await dbSicmaContext.Set<TEntity>()
                                .Where(predicate)
                                .AsNoTracking()
                                .ToListAsync();
            return result;
        }

        public async Task<ICollection<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await dbSicmaContext.Set<TEntity>()
                                .Where(predicate)
                                .AsNoTracking()
                                .Select(selector)
                                .ToListAsync();
            return result;
        }

        public async Task<(ICollection<TResult> Collection, int TotalRecords)> GetAllAsync<TResult, TKey>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int page =1, int rows =5)
        {
            var result = await dbSicmaContext.Set<TEntity>()
                                .Where(predicate)
                                .AsNoTracking()
                                .OrderBy(orderBy)
                                .Skip((page-1)*rows)
                                .Take(rows)
                                .Select(selector)
                                .ToListAsync();

            var totalRecords = await dbSicmaContext.Set<TEntity>()
                                      .Where(predicate)
                                      .CountAsync();
            return (result, totalRecords);
        }


        public async Task<TEntity?> FindByIdAsync(string id)
        {
            return await dbSicmaContext.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await dbSicmaContext.Set<TEntity>().AddAsync(entity);
            await dbSicmaContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateAsync()
        {
            await dbSicmaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await dbSicmaContext.Set<TEntity>()
                  .Where(p => p.Id == id)
                  .ExecuteUpdateAsync(
                    p => p.SetProperty(p => p.IsActive, false)
                );
        }
    }
}
