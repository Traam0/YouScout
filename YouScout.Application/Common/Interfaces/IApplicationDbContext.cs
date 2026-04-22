using Microsoft.EntityFrameworkCore;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
};