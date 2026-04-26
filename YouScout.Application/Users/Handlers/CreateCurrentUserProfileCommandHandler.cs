using MediatR;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;
using YouScout.Application.Users.Commands;
using YouScout.Domain.Common.Contracts;
using YouScout.Domain.Entities;

namespace YouScout.Application.Users.Handlers;

public class CreateCurrentUserProfileCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IUserContext currentUser)
    : IRequestHandler<CreateCurrentUserProfileCommand, Result>
{
    public async Task<Result> Handle(CreateCurrentUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (await repository.FindByIdentityUserIdAsync(currentUser.Id!, cancellationToken) is not null)
            return Result.Failure(["Profile already exists for this user."]);

        var user = User.Create(currentUser.Id!, request.FirstName, request.LastName);
        user.UpdateProfile(request.Bio, request.ProfilePicture);
        await repository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangeAsync(cancellationToken);
        return Result.Success();
    }
}