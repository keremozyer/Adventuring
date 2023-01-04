using Adventuring.Architecture.Data.Context.Implementation.EntityFramework.Extensions;
using Adventuring.Architecture.Data.Context.Interface;
using Adventuring.Architecture.Model.Entity.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Adventuring.Architecture.Data.Context.Implementation.EntityFramework;

/// <summary>
/// DataContext class to handle EntityFramework connections.
/// </summary>
public class BaseDataContext : DbContext, IDataContext
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="options"></param>
    public BaseDataContext(DbContextOptions options) : base(options) { }

    /// <inheritdoc/>
    public async Task Save(CancellationToken cancellationToken = default)
    {
        _ = await SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()?.Where(entityType => typeof(ISoftDeletedEntity).IsAssignableFrom(entityType.ClrType)) ?? Array.Empty<IMutableEntityType>())
        {
            entityType.AddSoftDeleteQueryFilter();
            entityType.AddIndex(entityType.FindProperty(nameof(ISoftDeletedEntity.DeletedAt))!)
                .IsUnique = true;
        }
    }
}