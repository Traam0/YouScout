using YouScout.Application.Common.Interfaces.Strategy;
using YouScout.Application.Feed;
using YouScout.Domain.Entities;
using YouScout.Domain.Enums;

namespace YouScout.Infrastructure.Strategy.Feed;

public class ForYouFeedStrategy : IFeedStrategy
{
    public IQueryable<PostScore> BuildQuery(IQueryable<Post> baseQuery, FeedContext context)
    {
        return baseQuery.Where(p => p.Status == PostStatus.Active)
            .Select(p => new PostScore()
            {
                Post = p,
                Score = (p.CreatedAt > DateTime.UtcNow.AddDays(-2) ? 5 : 0)
                        + (context.FollowingUserIds.Contains(Guid.Parse(p.UserId)) ? 10 : 0)
                        + (p.Hashtags.Count(h => context.FollowedTags.Contains(h))) * 2.5
                        + (p.Skills.Count(s => context.FollowedSkills.Contains(s))) * 3
                        - (context.RecentlySeenPostIds.Contains(p.Id) ? 8 : 0)
            }).OrderBy(ps => ps.Score)
            .ThenByDescending(ps => ps.Post.CreatedAt);
    }
}