namespace YouScout.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeRequestAttribute() : Attribute()
{
    public string Roles { get; set; } = string.Empty;

    public string Policy { get; set; } = string.Empty;
}