using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using YouScout.Application.Common.Interfaces;
using YouScout.Domain.Entities;

namespace YouScout.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHostEnvironment env)
    : IdentityDbContext(options), IApplicationDbContext
{
    public DbSet<User> Profiles { get; set; }
    public DbSet<Follow> Follows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(env.IsDevelopment() ? "dev" : "prod");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureMarker).Assembly);
    }
}