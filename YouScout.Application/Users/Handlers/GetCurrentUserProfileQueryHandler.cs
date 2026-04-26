using MediatR;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Users.Queries;
using YouScout.Domain.Common.Exceptions;
using YouScout.Domain.Entities;

namespace YouScout.Application.Users.Handlers;

public class GetCurrentUserProfileQueryHandler(IUserRepository repository, IUserContext currentUser)
    : IRequestHandler<GetCurrentUserProfileQuery, User>
{
    public async Task<User> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
    {
        return await repository.FindByIdentityUserIdAsync(currentUser.Id!, cancellationToken) ??
               throw new EntityNullReferenceException<User>(currentUser.Id!, "identityUserId");
    }
}