using System.Text.Json;
using YouScout.Application.Common.Exceptions;

namespace YouScout.Web.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (Exception ex)
        {
            var (statusCode, title, errors) = ex switch
            {
                ArgumentException a => (StatusCodes.Status400BadRequest, a.Message, null),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ex.Message, null),
                ForbiddenAccessException => (StatusCodes.Status403Forbidden, ex.Message, null),
                NotFoundException => (StatusCodes.Status404NotFound, ex.Message, null),
                ValidationException v => (StatusCodes.Status400BadRequest, v.Message, v.Errors),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.", null),
            };

            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = statusCode;

            var body = JsonSerializer.Serialize(new
            {
                title, errors,
                traceId = ctx.TraceIdentifier,
            });

            await ctx.Response.WriteAsync(body);
        }
    }
}