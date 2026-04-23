using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouScout.Domain.Entities;

namespace YouScout.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("app_users");

        builder.HasKey(u => u.Id)
            .HasName("pk_app_users_id");

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100);

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

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_app_users_email");

        builder.HasIndex(u => u.Username)
            .IsUnique()
            .HasDatabaseName("ux_app_users_username");

        builder.HasQueryFilter(u => u.DeletedAt == null);

        builder.Ignore(u => u.FullName);
    }
}