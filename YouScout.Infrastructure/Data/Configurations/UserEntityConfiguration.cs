using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouScout.Domain.Entities;
using YouScout.Infrastructure.Identity;

namespace YouScout.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user_profiles");

        builder.HasKey(u => u.Id)
            .HasName("pk_app_users_id");

        builder.Property(u => u.IdentityUserId)
            .HasColumnName("identity_user_id")
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.ProfilePictureUrl)
            .HasColumnName("profile_picture_url")
            .HasMaxLength(500);

        builder.Property(u => u.Bio)
            .HasColumnName("bio")
            .HasMaxLength(1000);

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at");

        builder.HasOne<AppUser>()
            .WithOne()
            .HasForeignKey<User>(u => u.IdentityUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Followers)
            .WithOne(f => f.FollowingUser)
            .HasForeignKey(f => f.FollowingUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Following)
            .WithOne(f => f.Follower)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasQueryFilter(u => u.DeletedAt == null);
        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("ux_app_users_username");

        builder.HasIndex(u => u.IdentityUserId)
            .IsUnique()
            .HasDatabaseName("ux_app_users_identity_user_id");

        builder.Ignore(u => u.FullName);
        builder.Ignore(u => u.Events);
    }
}