using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Users.Commands;
using YouScout.Application.Users.Queries;
using YouScout.Web.Models;

namespace YouScout.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            GetCurrentUserProfileQuery query = new();
            var response = await mediator.Send(query);
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (ForbiddenAccessException e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        GetProfileQuery query = new(Guid.Parse(id));
        var response = await mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfileRequest body)
    {
        var command = mapper.Map<CreateCurrentUserProfileCommand>(body);


        var response = await mediator.Send(command);
        if (response.Succeeded)
            return Created();
        return BadRequest(response.Errors);
    }

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}