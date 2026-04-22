namespace YouScout.Domain.Common.Contracts;

public interface IUnitOfWork
{
    bool SaveChanges();
    Task<bool> SaveChangeAsync(CancellationToken cancellationToken);
}