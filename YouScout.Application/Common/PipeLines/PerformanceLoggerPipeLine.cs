using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using YouScout.Application.Common.Interfaces;

namespace YouScout.Application.Common.PipeLines;

public partial class PerformanceLoggerPipeLine<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUserContext currentUser,
    IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _stopwatch = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        this._stopwatch.Start();
        var response = await next(cancellationToken);
        this._stopwatch.Stop();
        var elapsedMilliseconds = this._stopwatch.ElapsedMilliseconds;
        if (elapsedMilliseconds <= 500) return response;

        var username = string.Empty;

        if (!string.IsNullOrEmpty(currentUser.Id))
        {
            username = await identityService.GetUserNameAsync(currentUser.Id);
        }

        this.LogApplicationRequestPerformance(typeof(TRequest).Name, elapsedMilliseconds, currentUser.Id, username,
            request);
        return response;
    }

    [LoggerMessage(LogLevel.Warning,
        "Application Request<{RequestName}>: [{ElapsedMilliseconds} milliseconds], user({UserId}) -> {Username}.  {@Request}")]
    partial void LogApplicationRequestPerformance(string requestName, long elapsedMilliseconds, string? userId,
        string? username, TRequest @request);
}