using Serilog;
using YouScout.Application;
using YouScout.Infrastructure;
using YouScout.Infrastructure.Identity;
using YouScout.Web;
using YouScout.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services
    .RegisterApplicationLayer()
    .RegisterInfrastructureLayer(builder.Configuration)
    .RegisterWebServices();


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapIdentityApi<AppUser>();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();