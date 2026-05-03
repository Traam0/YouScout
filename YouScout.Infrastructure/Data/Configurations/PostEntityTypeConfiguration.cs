using YouScout.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouScout.Domain.Enums;
using YouScout.Infrastructure.Data.Converters;

namespace YouScout.Infrastructure.Data.Configurations;

public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        // Primary key
        builder.HasKey(p => p.Id)
            .HasName("pk_posts");

        // Properties
        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(p => p.VideoUrl)
            .HasColumnName("video_url")
            .IsRequired();

        builder.Property(p => p.ThumbnailUrl)
            .HasColumnName("thumbnail_url");

        builder.Property(p => p.Description)
            .HasColumnName("description");

        builder.Property(p => p.FileSizeBytes)
            .HasColumnName("file_size_bytes")
            .IsRequired();

        builder.Property(p => p.DurationSeconds)
            .HasColumnName("duration_seconds");

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasDefaultValue(PostStatus.Active);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(p => p.DeletedAt)
            .HasColumnName("deleted_at");


        builder.Property(p => p.Hashtags)
            .HasColumnName("hashtags")
            .HasConversion(new StringListConverter());

        builder.Property(p => p.Skills)
            .HasColumnName("skills")
            .HasConversion(new StringListConverter());

        builder.HasQueryFilter(p => p.DeletedAt == null);

        builder.Ignore("Events");
    }
}