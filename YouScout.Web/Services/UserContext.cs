using System.Security.Claims;
using YouScout.Application.Common.Interfaces;

namespace YouScout.Web.Services;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public IReadOnlyCollection<string> Roles => httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
        .Select(x => x.Value).ToList() ?? [];
}