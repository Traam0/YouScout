using YouScout.Application.Common.Interfaces.Strategy;
using YouScout.Application.Feed;
using YouScout.Domain.Entities;

namespace YouScout.Infrastructure.Strategy.Feed;

public class FollowingFeedStrategy : IFeedStrategy
{
    public IQueryable<PostScore> BuildQuery(IQueryable<Post> baseQuery, FeedContext context)
    {
        return baseQuery
            .Where(p => context.FollowingUserIds.Contains(Guid.Parse(p.UserId)))
            .Select(p => new PostScore
            {
                Post = p,
                Score = 100 + (p.CreatedAt.Ticks / 1_000_000_000.0)
            })
            .OrderByDescending(x => x.Score);
    }
}