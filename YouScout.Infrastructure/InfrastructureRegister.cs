using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouScout.Application.Common.Interfaces;
using YouScout.Domain.Common.Contracts;
using YouScout.Infrastructure.Data;
using YouScout.Infrastructure.Data.Interceptors;
using YouScout.Infrastructure.Identity;
using YouScout.Infrastructure.Interfaces.Storage;
using YouScout.Infrastructure.Persistence;
using YouScout.Infrastructure.Persistence.Repositories;
using YouScout.Infrastructure.Storage.Cloudinary;

namespace YouScout.Infrastructure;

public static class InfrastructureRegister
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, EventDispatcherInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            var connectionString = configuration.GetConnectionString("Remote") ??
                                   throw new Exception("Connection string 'Remote' not found.");
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();
        services
            .AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUserRepository, UserRepository>();

        services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
        services.AddScoped<IMediaStorage, CloudinaryMediaStorage>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}