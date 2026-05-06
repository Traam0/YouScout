namespace YouScout.Application.Feed;

public record FeedContext(
    string UserId,
    IEnumerable<string> FollowedTags,
    IEnumerable<string> FollowedSkills,
    IEnumerable<Guid> FollowingUserIds,
    IEnumerable<Guid> RecentlySeenPostIds
);