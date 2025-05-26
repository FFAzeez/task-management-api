using System.Linq.Expressions;
using TeamTaskManagementAPI.Domain.BindingModels;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Infrastructure.Persistence.Repositories;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions);

    Task<IReadOnlyList<TEntity>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter,
        bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    Task<PagedResult<TEntity>> GetPagedFilteredAsync(Expression<Func<TEntity, bool>> filter, int page, int pageSize, string sortColumn, string sortOrder,
         bool includeDeleted = false, params Expression<Func<TEntity, object>>[] includeExpressions);
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
    void UpdateAsync(TEntity entity);
    void UpdateRangeAsync(IEnumerable<TEntity> entities);
    void DeleteAsync(TEntity entity);
    void DeleteRangeAsync(IEnumerable<TEntity> entities);
}