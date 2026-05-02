using MediatR;
using YouScout.Application.Common.Models;
using YouScout.Application.Common.Security;

namespace YouScout.Application.Users.Queries;

[Guard]
public record GetProfileQuery(Guid UserId) : IRequest<ProfileDto>;