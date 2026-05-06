using MediatR;
using YouScout.Application.Common.Enums;
using YouScout.Application.Common.Models;
using YouScout.Application.Common.Models.Generic;
using YouScout.Application.Common.Security;

namespace YouScout.Application.Feed.Queries;

[Guard]
public record GetFeedQuery(
    FeedMode Mode,
    DateTimeOffset? Cursor = null,
    int Limit = 10,
    IEnumerable<string>? Hashtags = null,
    IEnumerable<string>? Skills = null,
    bool AvoidSeen = true) : IRequest<InfiniteScroll<PostDto>>;