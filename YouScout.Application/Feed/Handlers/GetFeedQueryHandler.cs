using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Interfaces.Strategy;
using YouScout.Application.Common.Models;
using YouScout.Application.Common.Models.Generic;
using YouScout.Application.Feed.Queries;

namespace YouScout.Application.Feed.Handlers;

public class GetFeedQueryHandler(
    IApplicationDbContext context,
    IUserContext currentUser,
    IFeedStrategyFactory strategyFactory,
    IMapper mapper) : IRequestHandler<GetFeedQuery, InfiniteScroll<PostDto>>
{
    public async Task<InfiniteScroll<PostDto>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
    {
        var followedHashTagsTask =
            context.Posts.Where(p => p.UserId == currentUser.Id!).SelectMany(p => p.Hashtags).Distinct()
                .ToListAsync(cancellationToken);

        var followedSkillsTask =
            context.Posts.Where(p => p.UserId == currentUser.Id!).SelectMany(p => p.Skills)
                .Distinct()
                .ToListAsync(cancellationToken);

        var followingUserIdsTask = context.Follows.Where(f => f.FollowerId == Guid.Parse(currentUser.Id!))
            .Select(f => f.FollowingUserId).ToListAsync(cancellationToken);


        if (request.AvoidSeen == true) throw new NotImplementedException("Seen Posts Tracking is not yet implemented");

        await Task.WhenAll(followingUserIdsTask, followedSkillsTask, followedHashTagsTask);
        var followedHashTags = await followedHashTagsTask;
        var followedSkills = await followedSkillsTask;
        var followingUserIds = await followingUserIdsTask;

        FeedContext feedContext = new(currentUser.Id!, followedHashTags, followedSkills, followingUserIds, []);
        var baseQuery = context.Posts.AsNoTracking();
        var strategy = strategyFactory.GetStrategy(request.Mode);
        var rankedQuery = strategy.BuildQuery(baseQuery, feedContext);

        if (request.Cursor.HasValue) rankedQuery.Where(x => x.Post.CreatedAt < request.Cursor.Value);

        var results = await rankedQuery.Take(request.Limit).Select(x => mapper.Map<PostDto>(x.Post))
            .ToListAsync(cancellationToken);

        return InfiniteScroll<PostDto>.Create(
            results,
            request.Limit,
            p => p.CreatedAt,
            cursor => cursor.ToString("O")
        );
    }
}