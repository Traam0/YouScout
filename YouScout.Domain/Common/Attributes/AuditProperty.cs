namespace YouScout.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class AuditProperty(string? altName = null) : Attribute
{
    public string? AltName { get; init; } = altName;
}