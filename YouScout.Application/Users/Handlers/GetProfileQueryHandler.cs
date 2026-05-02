using MediatR;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;
using YouScout.Application.Users.Queries;
using YouScout.Domain.Entities;

namespace YouScout.Application.Users.Handlers;

public class GetProfileQueryHandler(IUserQueries userQueries) : IRequestHandler<GetProfileQuery, ProfileDto>
{
    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await userQueries.GetProfileAsync(request.UserId, cancellationToken) ?? throw
            NotFoundException.For<User>(request.UserId);
    }
}