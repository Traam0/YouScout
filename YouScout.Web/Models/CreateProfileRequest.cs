namespace YouScout.Web.Models;

public record CreateProfileRequest(
    string FirstName,
    string LastName,
    string? Bio,
    string? ProfilePicture
);