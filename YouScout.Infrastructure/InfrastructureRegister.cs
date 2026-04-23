using Microsoft.Extensions.DependencyInjection;

namespace YouScout.Infrastructure;

public static class InfrastructureRegister
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services)
    {
        return services;
    }
}