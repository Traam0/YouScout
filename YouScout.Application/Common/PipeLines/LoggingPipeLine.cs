using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using YouScout.Application.Common.Interfaces;

namespace YouScout.Application.Common.PipeLines;

public partial class LoggingPipeLine<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUserContext currentUser,
    IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var username = string.Empty;

        if (!string.IsNullOrEmpty(currentUser.Id))
        {
            username = await identityService.GetUserNameAsync(currentUser.Id);
        }

        this.LogApplicationRequestRequestNameUserUseridUsername(
            typeof(TRequest).Name,
            currentUser.Id,
            username,
            request
        );
        return await next();
    }

    [LoggerMessage(LogLevel.Information,
        "Application Request<{RequestName}>: user({UserId}) -> {Username}. {@Request}")]
    partial void LogApplicationRequestRequestNameUserUseridUsername(string requestName, string? userId,
        string? username, TRequest @request);
}