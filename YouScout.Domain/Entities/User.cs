using YouScout.Domain.Common.Abstract;
using YouScout.Domain.Common.Contracts;

namespace YouScout.Domain.Entities;

public class User : AuditableEntity, ISoftDeletable
{
    public Guid Id { get; init; }
    public string IdentityUserId { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string? Bio { get; private set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public User(string firstName, string lastName, string identityUserId)
    {
        Id = Guid.NewGuid();
        IdentityUserId = identityUserId;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = DateTime.UtcNow;
    }


    public string FullName => $"{FirstName} {LastName}";
}