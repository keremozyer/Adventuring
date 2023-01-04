using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game.TestsGameService;

public class StartGameTests : BaseGameTestFixture
{
    [Test]
    public async Task StartGame_Success_NotEndNode()
    {
        string createdGamesID = "createdGamesID";
        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Create(It.IsAny<Game>())).Returns(Task.FromResult(createdGamesID));

        GameService gameService = new(base.HappyPathAdventureTreeServiceMock.Object, this.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper);
        StartGameOutputModel result = await gameService.StartGame(new StartGameInputModel() { AdventureID = ValidAdventureID, FirstAnswer = true });

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.ID, Is.EqualTo(createdGamesID));
            Assert.That(result.IsEndNode, Is.False);
            Assert.That(result.NextMessage, Is.EqualTo(this.ValidAdventure.StartingNode.PositiveAnswerNode.NodeMessage));
        });
    }

    [Test]
    public async Task StartGame_Success_EndNode()
    {
        string createdGamesID = "createdGamesID";

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Create(It.IsAny<Game>())).Returns(Task.FromResult(createdGamesID));

        GameService gameService = new(base.HappyPathAdventureTreeServiceMock.Object, this.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper);
        StartGameOutputModel result = await gameService.StartGame(new StartGameInputModel() { AdventureID = ValidAdventureID, FirstAnswer = false });

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.ID, Is.EqualTo(createdGamesID));
            Assert.That(result.IsEndNode, Is.True);
            Assert.That(result.NextMessage, Is.EqualTo(this.ValidAdventure.StartingNode.NegativeAnswerNode.NodeMessage));
        });
    }
}