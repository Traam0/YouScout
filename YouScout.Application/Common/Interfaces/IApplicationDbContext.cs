using Microsoft.EntityFrameworkCore;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Profiles { get; }
    DbSet<Follow> Follows { get; }
    DbSet<Post> Posts { get; }
};