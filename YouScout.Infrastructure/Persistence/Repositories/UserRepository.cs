using Microsoft.EntityFrameworkCore;
using YouScout.Application.Common.Interfaces;
using YouScout.Domain.Entities;
using YouScout.Infrastructure.Data;

namespace YouScout.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : Repository<User, Guid>(context), IUserRepository
{
    public async Task<User?> FindByIdentityUserIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await base.Context.Users.Where(u => u.IdentityUserId == id).FirstOrDefaultAsync(cancellationToken);
    }
}