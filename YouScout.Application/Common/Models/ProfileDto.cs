namespace YouScout.Application.Common.Models;

public class ProfileDto
{
    public string Id { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string? Bio { get; init; }
    public string? Avatar { get; init; }
    public int Posts { get; init; }
    public int Followers { get; init; }
    public int Following { get; init; }
}