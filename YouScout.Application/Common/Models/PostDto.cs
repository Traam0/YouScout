namespace YouScout.Application.Common.Models;

public class PostDto
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public string VideoUrl { get; init; }
    public string? ThumbnailUrl { get; init; }
    public string? Description { get; init; }
    public List<string> Hashtags { get; init; }
    public List<string> Skills { get; init; }
    public long FileSizeBytes { get; init; }
    public int DurationSeconds { get; init; }
    public string Status { get; init; }
    public DateTime CreatedAt { get; init; }
}