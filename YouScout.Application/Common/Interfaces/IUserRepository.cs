using YouScout.Domain.Common.Contracts;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> FindByIdentityUserIdAsync(string id, CancellationToken cancellationToken = default);
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> FindByIdWithStats(Guid id, CancellationToken cancellationToken = default);
}