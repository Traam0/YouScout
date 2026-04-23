using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;

namespace YouScout.Infrastructure.Identity;

public class IdentityService(
    UserManager<AppUser> userManager,
    IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService) : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);
        return user is not null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null) return false;

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);
        var result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password)
    {
        var user = new AppUser()
        {
            UserName = userName,
            Email = email,
        };

        var response = await userManager.CreateAsync(user, password);

        var result = response.Succeeded
            ? Result.Success()
            : Result.Failure(response.Errors.Select(e => e.Description));

        return (result, user.Id);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    private async Task<Result> DeleteUserAsync(AppUser user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }
}