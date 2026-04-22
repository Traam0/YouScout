using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using YouScout.Domain.Common.Attributes;

namespace YouScout.Domain.Common.Abstract;

public abstract class AuditableEntity : Entity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }


    public bool TryGetSecurityTag(out string? value)
    {
        //TODO Impl After adding Security Tag Prop Attribute
        var properties = CollectionsMarshal.AsSpan(
            GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.IsDefined(typeof(AuditProperty))).ToList()
        );

        if (properties.IsEmpty)
        {
            value = null;
            return false;
        }

        StringBuilder stringBuilder = new();
        foreach (var propertyInfo in properties)
        {
            string propertyName = propertyInfo.GetCustomAttribute<AuditProperty>()?.AltName ?? propertyInfo.Name;
            stringBuilder.Append(propertyName)
                .Append(">>")
                .Append(propertyInfo.GetValue(this))
                .AppendLine(">>>");
        }

        var bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        stringBuilder.Clear();

        for (var i = 0; i < bytes.Length; i++)
        {
            var @byte = bytes[i];
            
            int n = @byte;
            var n1 = n & 15;
            var n2 = (n >> 4) & 15;
            
            if (n2 > 9) stringBuilder.Append(n2 - 10 + 'A');
            else stringBuilder.Append(n2);
            if (n1 > 9) stringBuilder.Append(n1 - 10 + 'A');
            else stringBuilder.Append(n1);
            if ((i + 1) != bytes.Length && (i + 1) % 2 == 0) stringBuilder.Append('-');
        }

        value = stringBuilder.ToString();
        return true;
    }
}