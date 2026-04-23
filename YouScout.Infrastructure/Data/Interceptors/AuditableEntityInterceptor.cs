using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using YouScout.Domain.Common.Abstract;

namespace YouScout.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        var entries =
            CollectionsMarshal.AsSpan(context.ChangeTracker.Entries<AuditableEntity>()?.ToList());
        if (entries.Length == 0) return;

        foreach (var entry in entries)
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified) &&
                !entry.HasChangedOwnedEntities()) continue;
            var utcNow = DateTimeOffset.UtcNow;
            if (entry.State is EntityState.Added) entry.Entity.CreatedAt = utcNow;
            if (entry.State is not EntityState.Modified) continue;
            if (entry.Entity.TryGetSecurityTag(out var securityHash))
            {
                var hash = entry.Entity.GetType()
                    .GetProperties()
                    .FirstOrDefault(p => p.Name == "Hash");

                if (hash != null && securityHash != null && securityHash.Equals(hash.GetValue(entry.Entity)))
                {
                    hash.SetValue(entry.Entity, securityHash);
                }
            }

            entry.Entity.UpdatedAt = utcNow;
        }
    }
}

internal static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}