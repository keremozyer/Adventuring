using Adventuring.Contexts.AdventureManager.Concern.Option.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.Mappers.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;
using Adventuring.Contexts.AdventureManager.Test.Unit._Base;
using AutoMapper;

namespace Adventuring.Contexts.AdventureManager.Test.Unit.Adventure;

public abstract class BaseAdventureTestFixture : BaseAdventureManagerTestFixture
{
    protected readonly AdventureSettings AdventureSettings = new()
    {
        AdventureCreatorRole = "Designer"
    };

    protected readonly IAdventureMapper AdventureMapper = new AdventureMapper(new MapperConfiguration(configuration => configuration.AddProfile(new AdventureManagerAutoMapperProfile())).CreateMapper());

    protected AdventureTree ThreeWithOneLayer;
    protected AdventureTree ThreeWithThreeLayer;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        this.ThreeWithOneLayer = GetTreeWithOneLayer();
        this.ThreeWithThreeLayer = GetTreeWithThreeLayer();
    }

    private static AdventureTree GetTreeWithOneLayer()
    {
        AdventureTree adventureTree = new("AdventureName", "Starting Node");

        adventureTree.StartingNode.AddPositiveAnswerNode("positive");
        adventureTree.StartingNode.AddNegativeAnswerNode("negative");

        return adventureTree;
    }

    private static AdventureTree GetTreeWithThreeLayer()
    {
        AdventureTree adventureTree = new("AdventureName", "Starting Node");

        adventureTree.StartingNode.AddPositiveAnswerNode("positive");
        adventureTree.StartingNode.AddNegativeAnswerNode("negative");

        adventureTree.StartingNode.PositiveAnswerNode.AddPositiveAnswerNode("positive->positive");
        adventureTree.StartingNode.PositiveAnswerNode.AddNegativeAnswerNode("positive->negative");
        adventureTree.StartingNode.NegativeAnswerNode.AddPositiveAnswerNode("negative->positive");
        adventureTree.StartingNode.NegativeAnswerNode.AddNegativeAnswerNode("negative->negative");

        adventureTree.StartingNode.PositiveAnswerNode.PositiveAnswerNode.AddPositiveAnswerNode("positive->positive->positive");
        adventureTree.StartingNode.PositiveAnswerNode.PositiveAnswerNode.AddNegativeAnswerNode("positive->positive->negative");
        adventureTree.StartingNode.PositiveAnswerNode.NegativeAnswerNode.AddPositiveAnswerNode("positive->negative->positive");
        adventureTree.StartingNode.PositiveAnswerNode.NegativeAnswerNode.AddNegativeAnswerNode("positive->negative->negative");
        adventureTree.StartingNode.NegativeAnswerNode.PositiveAnswerNode.AddPositiveAnswerNode("negative->positive->positive");
        adventureTree.StartingNode.NegativeAnswerNode.PositiveAnswerNode.AddNegativeAnswerNode("negative->positive->negative");
        adventureTree.StartingNode.NegativeAnswerNode.NegativeAnswerNode.AddPositiveAnswerNode("negative->negative->positive");
        adventureTree.StartingNode.NegativeAnswerNode.NegativeAnswerNode.AddNegativeAnswerNode("negative->negative->negative");

        return adventureTree;
    }
}