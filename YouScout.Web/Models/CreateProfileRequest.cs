namespace YouScout.Web.Models;

public record CreateProfileRequest(
    string Username,
    string FirstName,
    string LastName,
    string? Bio,
    string? ProfilePicture
);