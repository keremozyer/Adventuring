using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Contexts.UserManager.Model.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Context;

/// <summary>
/// EntityFramework DataContext class to handle UserManager database operations.
/// </summary>
public class UserManagerDataContext : BaseDataContext
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="options"></param>
    public UserManagerDataContext(DbContextOptions options) : base(options) { }

    /// <summary></summary>
    public DbSet<AppUser> AppUsers { get; set; }
    /// <summary></summary>
    public DbSet<Role> Roles { get; set; }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<AppUser>(configuration =>
        {
            _ = configuration.Property(appUser => appUser.Username)
                .IsRequired(true)
                .HasMaxLength(32);
            _ = configuration.HasIndex(nameof(AppUser.DeletedAt), nameof(AppUser.Username)).IsUnique(true);

            _ = configuration.Property(appUser => appUser.Password)
                .IsRequired(true)
                .HasMaxLength(128);

            _ = configuration.Property(appUser => appUser.Salt)
                .IsRequired(true)
                .HasMaxLength(88);

            _ = configuration.HasMany(appUser => appUser.Roles).WithMany(role => role.AppUsers);
        });
        _ = modelBuilder.Entity<Role>(configuration =>
        {
            _ = configuration.Property(role => role.Name)
                .IsRequired(true)
                .HasMaxLength(32);
            _ = configuration.HasIndex(nameof(Role.DeletedAt), nameof(Role.Name)).IsUnique(true);

            _ = configuration.HasMany(role => role.AppUsers).WithMany(appUser => appUser.Roles);
        });
    }
}