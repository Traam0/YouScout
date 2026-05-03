using MediatR;
using Microsoft.AspNetCore.Http;
using YouScout.Application.Common.Models;
using YouScout.Application.Common.Security;

namespace YouScout.Application.Posts.Commands;

[Guard]
public record CreatePostCommand(
    IFormFile Video,
    string? Description,
    IEnumerable<string> Hashtags,
    IEnumerable<string> Skills
) : IRequest<PostDto>;