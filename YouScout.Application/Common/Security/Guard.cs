namespace YouScout.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class Guard() : Attribute()
{
    public string Roles { get; set; } = string.Empty;

    public string Policy { get; set; } = string.Empty;
}