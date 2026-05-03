using System.Reflection;
using Microsoft.OpenApi;
using YouScout.Application.Common.Interfaces;
using YouScout.Web.Services;

namespace YouScout.Web;

public static class WebRegister
{
    public static IServiceCollection RegisterWebServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "Enter the token returned from /login"
            });

            options.AddSecurityRequirement(document =>
            {
                var requirement = new OpenApiSecurityRequirement
                    { { new OpenApiSecuritySchemeReference("Bearer", document), [] } };
                return requirement;
            });
        });
        services.AddCors(opts =>
            opts.AddDefaultPolicy(p =>
                p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

        return services;
    }
}