using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Enums;

namespace YouScout.Domain.Entities;

public class Follow : AuditableEntity
{
    public Guid FollowerId { get; private set; }
    public Guid FollowingUserId { get; private set; }

    public FollowType FollowType { get; private set; }

    public User Follower { get; private set; } = null!;
    public User? FollowingUser { get; private set; }
    
    private Follow(){}

    public static Follow Create(
        Guid followerId,
        Guid followingUserId,
        FollowType followType
    )
    {
        return new Follow()
        {
            FollowerId = followerId,
            FollowingUserId = followingUserId,
            FollowType = followType,
        };
    }
}