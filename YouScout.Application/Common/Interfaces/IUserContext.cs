namespace YouScout.Application.Common.Interfaces;

public interface IUserContext
{
    string? Id { get; }
    IReadOnlyCollection<string> Roles { get; }
}