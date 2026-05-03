using MediatR;
using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Common.Contracts;
using YouScout.Domain.Enums;

namespace YouScout.Domain.Entities;

public class Post : AuditableEntity, ISoftDeletable
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = default!;
    public string VideoUrl { get; private set; } = default!;
    public string? ThumbnailUrl { get; private set; }
    public string? Description { get; private set; }
    public List<string> Hashtags { get; private set; } = [];
    public List<string> Skills { get; private set; } = [];
    public long FileSizeBytes { get; private set; }
    public int DurationSeconds { get; private set; }
    public PostStatus Status { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

    private Post()
    {
    }

    public static Post Create(
        string userId,
        string videoUrl,
        string? description,
        List<string> hashtags,
        List<string> skills,
        long fileSizeBytes,
        int durationSeconds = 0,
        string? thumbnailUrl = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentException.ThrowIfNullOrWhiteSpace(videoUrl);

        return new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            VideoUrl = videoUrl,
            ThumbnailUrl = thumbnailUrl,
            Description = description?.Trim(),
            Hashtags = hashtags,
            Skills = skills,
            FileSizeBytes = fileSizeBytes,
            DurationSeconds = durationSeconds,
            Status = PostStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public void SetThumbnail(string thumbnailUrl)
    {
        ThumbnailUrl = thumbnailUrl;
        UpdatedAt = DateTime.UtcNow;
    }


    public void Delete()
    {
        if (this.DeletedAt.HasValue) return;
        this.DeletedAt = DateTimeOffset.UtcNow;
    }

    public void Restore()
    {
        if (!this.DeletedAt.HasValue) return;
        this.DeletedAt = null;
    }
}