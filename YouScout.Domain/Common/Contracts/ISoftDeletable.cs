namespace YouScout.Domain.Common.Contracts;

public interface ISoftDeletable
{
    DateTimeOffset? DeletedAt { get; set; }
}