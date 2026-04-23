using Microsoft.EntityFrameworkCore;
using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Common.Contracts;
using YouScout.Infrastructure.Data;

namespace YouScout.Infrastructure.Persistence;

public class Repository<TEntity, TKey>(ApplicationDbContext context) : IRepository<TEntity, TKey>
    where TEntity : Entity
    where TKey : notnull
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>()
            .AsNoTracking()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await context.Set<TEntity>().AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var entry = context.Set<TEntity>().Update(entity);
            return entry.Entity;
        }, cancellationToken);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var entry = context.Set<TEntity>().Remove(entity);
            return entry.Entity;
        }, cancellationToken);
    }
}