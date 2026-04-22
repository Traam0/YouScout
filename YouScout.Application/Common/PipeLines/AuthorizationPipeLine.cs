using System.Reflection;
using MediatR;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Security;

namespace YouScout.Application.Common.PipeLines;

public class AuthorizationPipeLine<TRequest, TResponse>(IUserContext currentUser, IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType().GetCustomAttributes<AuthorizeRequestAttribute>().ToList();

        if (authorizationAttributes.Count == 0) return await next(cancellationToken);
        if (currentUser.Id is null) throw new UnauthorizedAccessException();

        var roleAuthorizationAttributes =
            authorizationAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles)).ToList();

        if (roleAuthorizationAttributes.Count != 0 &&
            !roleAuthorizationAttributes
                .SelectMany(attribute => attribute.Roles.Split(','))
                .Select(ar => ar.Trim())
                .Any(ar => currentUser.Roles.Contains(ar, StringComparer.OrdinalIgnoreCase))
           ) throw new ForbiddenAccessException();

        var policyAuthorizationAttributes =
            authorizationAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy)).ToList();

        foreach (var policy in policyAuthorizationAttributes.Select(a => a.Policy))
        {
            var authorized = await identityService.AuthorizeAsync(currentUser.Id, policy);
            if (!authorized) throw new ForbiddenAccessException();
        }

        return await next(cancellationToken);
    }
}