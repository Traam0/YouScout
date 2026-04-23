using YouScout.Domain.Common.Contracts;
using YouScout.Infrastructure.Data;

namespace YouScout.Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<bool> SaveChangeAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken) != 0;
    }

    public bool SaveChanges()
    {
        return context.SaveChanges() != 0;
    }
}