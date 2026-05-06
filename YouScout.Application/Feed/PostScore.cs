using YouScout.Domain.Entities;

namespace YouScout.Application.Feed;

public class PostScore
{
    public Post Post { get; set; } = null!;
    public double Score { get; set; }
}