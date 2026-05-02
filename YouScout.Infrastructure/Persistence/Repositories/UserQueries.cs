using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;
using YouScout.Domain.Entities;
using YouScout.Infrastructure.Data;

namespace YouScout.Infrastructure.Persistence.Repositories;

public class UserQueries(ApplicationDbContext context, IMapper mapper) : IUserQueries
{
    public async Task<User?> FindByIdentityUserIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.Profiles.Where(u => u.IdentityUserId == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> FindByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await context.Profiles
            .Where(u => EF.Functions.Like(u.Username, username))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ProfileDto?> GetProfileAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Profiles.Where(p => p.Id == id)
            .ProjectTo<ProfileDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}