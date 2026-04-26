using System.Runtime.CompilerServices;
using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Common.Contracts;

namespace YouScout.Domain.Entities;

public class User : AuditableEntity, ISoftDeletable
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string IdentityUserId { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? ProfilePictureUrl { get; private set; } = null;
    public string? Bio { get; private set; } = null;
    public string FullName => $"{FirstName} {LastName}";
    public DateTimeOffset? DeletedAt { get; private set; }

    private User()
    {
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


    public void UpdateProfile(string? bio, string? profilePictureUrl)
    {
        if (!string.Equals(this.Bio, bio, StringComparison.OrdinalIgnoreCase))
            this.Bio = bio;
        if (!string.Equals(this.ProfilePictureUrl, profilePictureUrl, StringComparison.OrdinalIgnoreCase))
            this.Bio = bio;
    }

    public void UpdateProfile(string firstName, string lastName, string bio, string? profilePictureUrl)
    {
        if (!this.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)) this.FirstName = firstName;
        if (!this.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) this.LastName = lastName;
        this.UpdateProfile(bio, profilePictureUrl);
    }


    public static User Create(string identityUserId, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(identityUserId))
            throw new ArgumentException("IdentityUserId is required");

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required");

        return new User()
        {
            IdentityUserId = identityUserId,
            FirstName = firstName,
            LastName = lastName,
        };
    }
}