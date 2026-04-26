using MediatR;
using YouScout.Application.Common.Security;
using YouScout.Domain.Entities;

namespace YouScout.Application.Users.Queries;

[Guard]
public record GetCurrentUserProfileQuery() : IRequest<User>;