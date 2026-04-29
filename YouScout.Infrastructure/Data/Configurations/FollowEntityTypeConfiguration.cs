using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouScout.Domain.Entities;
using YouScout.Domain.Enums;

namespace YouScout.Infrastructure.Data.Configurations;

public class FollowEntityTypeConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable("follows");

        builder.HasKey(f => new { f.FollowerId, f.FollowingUserId })
            .HasName("pk_follows");

        builder.Property(f => f.FollowerId)
            .HasColumnName("follower_id")
            .IsRequired();

        builder.Property(f => f.FollowingUserId)
            .HasColumnName("following_user_id")
            .IsRequired();

        builder.Property(f => f.FollowType)
            .HasColumnName("follow_type")
            .HasConversion<string>()
            .HasDefaultValue(FollowType.User);
        
        builder.Property(f => f.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(f => f.UpdatedAt)
            .HasColumnName("updated_at");


        builder.Ignore(f => f.Events);
    }
}