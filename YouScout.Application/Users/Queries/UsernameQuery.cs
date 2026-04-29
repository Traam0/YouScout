using MediatR;

namespace YouScout.Application.Users.Queries;

public record UsernameQuery(string Username): IRequest<bool>;