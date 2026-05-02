using MediatR;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Users.Queries;

namespace YouScout.Application.Users.Handlers;

public class UsernameQueryHandler(IUserQueries queries) : IRequestHandler<UsernameQuery, bool>
{
    public async Task<bool> Handle(UsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await queries.FindByUsernameAsync(request.Username, cancellationToken);
        return user != null;
    }
}