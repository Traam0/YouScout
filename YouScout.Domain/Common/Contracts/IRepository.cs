using YouScout.Domain.Common.Abstract;

namespace YouScout.Domain.Common.Contracts;

public interface IRepository<TEntity, in TKey> where TEntity : Entity where TKey : notnull
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}