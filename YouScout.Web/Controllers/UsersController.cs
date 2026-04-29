using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouScout.Application.Users.Queries;

namespace YouScout.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("check/{username}")]
    public async Task<IActionResult> Check([FromRoute] string username, CancellationToken cancellationToken = default)
    {
        UsernameQuery query = new(username);
        return await mediator.Send(query, cancellationToken) ? BadRequest() : Ok();
    }
}