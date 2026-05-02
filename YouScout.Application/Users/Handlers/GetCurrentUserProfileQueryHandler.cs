using MediatR;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;
using YouScout.Application.Users.Queries;
using YouScout.Domain.Entities;

namespace YouScout.Application.Users.Handlers;

public class GetCurrentUserProfileQueryHandler(IUserQueries userQueries, IUserContext currentUser)
    : IRequestHandler<GetCurrentUserProfileQuery, ProfileDto>
{
    public async Task<ProfileDto> Handle(GetCurrentUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userQueries.FindByIdentityUserIdAsync(currentUser.Id!, cancellationToken) ??
                   throw NotFoundException.For<User>(currentUser.Id!, "identityUserId");

        return await userQueries.GetProfileAsync(user.Id, cancellationToken) ??
               throw NotFoundException.For<User>(user.Id!);
    }
}