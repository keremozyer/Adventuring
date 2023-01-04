using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;
using Microsoft.EntityFrameworkCore;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.EntityFramework.Context;

public class AdventureManagerDataContext : BaseDataContext
{
    public AdventureManagerDataContext(DbContextOptions options) : base(options) { }

    public DbSet<AdventureTree> AdventureTrees { get; set; }
    public DbSet<AdventureNode> AdventureNodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<AdventureTree>(configuration =>
        {
            _ = configuration
                .HasIndex(adventureTree => adventureTree.AdventureName)
                .IsUnique(true);
            _ = configuration
                .HasOne(adventureTree => adventureTree.StartingNode).WithOne(startingNode => startingNode.AdventureTree);
        });
        _ = modelBuilder.Entity<AdventureNode>(configuration =>
        {
            _ = configuration
                .HasOne(adventureNode => adventureNode.AdventureTree).WithOne(adventureTree => adventureTree.StartingNode);
            _ = configuration
                .HasOne(adventureNode => adventureNode.PositiveAnswerNode).WithOne(adventureNode => adventureNode.ParentNode)
                .IsRequired(false);
            _ = configuration
                .HasOne(adventureNode => adventureNode.NegativeAnswerNode).WithOne(adventureNode => adventureNode.ParentNode)
                .IsRequired(false);
            _ = configuration
                .HasIndex(adventureNode => adventureNode.AdventureTreeID)
                .IsUnique(false);
            _ = configuration
                .HasIndex(adventureNode => adventureNode.ParentNodeID)
                .IsUnique(false);
            _ = configuration
                .Property(adventureNode => adventureNode.AdventureTreeID)
                .IsRequired(true);
            _ = configuration
                .Property(adventureNode => adventureNode.ParentNodeID)
                .IsRequired(true);
        });
    }
}