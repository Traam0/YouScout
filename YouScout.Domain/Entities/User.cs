using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Common.Contracts;

namespace YouScout.Domain.Entities;

public class User : AuditableEntity, ISoftDeletable
{
    public Guid Id { get; init; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string? Bio { get; private set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public User(string username, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }


    public string FullName => $"{FirstName} {LastName}";
}