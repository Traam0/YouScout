using MediatR;
using Microsoft.Extensions.Logging;
using YouScout.Application.Common.Exceptions;

namespace YouScout.Application.Common.PipeLines;

public partial class UnhandledExceptionPipeLine<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            this.LogCriticalExceptionRequest(typeof(TRequest).Name, request, e);
            throw;
        }
    }

    [LoggerMessage(LogLevel.Critical, "Application Request<{RequestName}>: Unhandled exception: {@Request}")]
    partial void LogCriticalExceptionRequest(string requestName, TRequest @request,
        Exception exception);
}