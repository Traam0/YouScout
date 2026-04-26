using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using YouScout.Application.Common.Exceptions;
using YouScout.Application.Users.Commands;
using YouScout.Application.Users.Queries;
using YouScout.Domain.Common.Exceptions;
using YouScout.Domain.Entities;
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
        catch (EntityNullReferenceException<User> ex)
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


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfileRequest body)
    {
        try
        {
            var command = mapper.Map<CreateCurrentUserProfileCommand>(body);
            
            
            var response = await mediator.Send(command);
            if (response.Succeeded)
                return Created();
            return BadRequest(response.Errors);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message, errors = ex.Errors });
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

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}


