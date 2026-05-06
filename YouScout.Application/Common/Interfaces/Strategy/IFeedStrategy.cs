using YouScout.Application.Feed;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Interfaces.Strategy;

public interface IFeedStrategy
{
    IQueryable<PostScore> BuildQuery(
        IQueryable<Post> baseQuery,
        FeedContext context
    );
}