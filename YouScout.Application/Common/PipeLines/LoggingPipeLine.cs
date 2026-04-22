using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using YouScout.Application.Common.Interfaces;

namespace YouScout.Application.Common.PipeLines;

public partial class LoggingPipeLine<TRequest>(
    ILogger<TRequest> logger,
    IUserContext currentUser,
    IIdentityService identityService)
    : IRequestPreProcessor<TRequest> where TRequest : IRequest
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
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
    }

    [LoggerMessage(LogLevel.Information,
        "Application Request<{RequestName}>: user({UserId}) -> {Username}. {@Request}")]
    partial void LogApplicationRequestRequestNameUserUseridUsername(string requestName, string? userId,
        string? username, TRequest @request);
}