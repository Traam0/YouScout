using YouScout.Application.Common.Models;
using YouScout.Domain.Common.Contracts;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Interfaces;

public interface IUserQueries
{
    Task<User?> FindByIdentityUserIdAsync(string id, CancellationToken cancellationToken = default);
    Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<ProfileDto?> GetProfileAsync(Guid id, CancellationToken cancellationToken = default);
}