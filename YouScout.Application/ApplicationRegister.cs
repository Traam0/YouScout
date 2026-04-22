using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Common.PipeLines;

namespace YouScout.Application;

public static class ApplicationRegister
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddMaps(typeof(ApplicationMarker).Assembly));
        services.AddValidatorsFromAssembly(typeof(ApplicationMarker).Assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationMarker).Assembly);

            config.AddOpenRequestPreProcessor(typeof(LoggingPipeLine<>));
            config.AddOpenBehavior(typeof(UnhandledExceptionPipeLine<,>));
            config.AddOpenBehavior(typeof(AuthorizationPipeLine<,>));
            config.AddOpenBehavior(typeof(ValidationPipeLine<,>));
            config.AddOpenBehavior(typeof(PerformanceLoggerPipeLine<,>));
        });
        return services;
    }
}