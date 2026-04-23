using YouScout.Application;
using YouScout.Infrastructure;
using YouScout.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterApplicationLayer()
    .RegisterInfrastructureLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapIdentityApi<AppUser>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();