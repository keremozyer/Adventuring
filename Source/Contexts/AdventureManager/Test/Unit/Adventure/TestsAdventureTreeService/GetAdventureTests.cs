using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit.Adventure.TestsAdventureTreeService;

public class GetAdventureTests : BaseAdventureTestFixture
{
    [Test]
    public async Task GetAdventure_Success()
    {
        AdventureTree tree = new("id", "aa", "bb");

        Mock<IAdventureDataStore> adventureDataStoreMock = new();
        _ = adventureDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(tree));

        AdventureTreeService adventureTreeService = new(base.AdventureMapper, adventureDataStoreMock.Object, new Mock<IActiveUser>().Object, base.AdventureSettings);

        GetAdventureOutputModel result = await adventureTreeService.Get(tree.ID);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(tree.ID));
        Assert.That(result.AdventureName, Is.EqualTo(tree.AdventureName));
        Assert.That(result.StartingNode, Is.Not.Null);
        Assert.That(result.StartingNode.NodeMessage, Is.EqualTo(tree.StartingNode.NodeMessage));
    }

    [Test]
    public void GetAdventure_NotExists()
    {
        Mock<IAdventureDataStore> adventureDataStoreMock = new();
        _ = adventureDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AdventureTree>(null));

        AdventureTreeService adventureTreeService = new(base.AdventureMapper, adventureDataStoreMock.Object, new Mock<IActiveUser>().Object, base.AdventureSettings);

        Assert.That(async () => await adventureTreeService.Get("aa"),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(AdventureTree)));
    }
}