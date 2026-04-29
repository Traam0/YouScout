using MediatR;
using YouScout.Application.Common.Models;
using YouScout.Application.Common.Security;

namespace YouScout.Application.Users.Commands;

[Guard]
public record CreateCurrentUserProfileCommand(string Username,string FirstName, string LastName, string? Bio, string ProfilePicture)
    : IRequest<Result>;