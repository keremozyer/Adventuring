using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game.TestsGameService;

public class GetGameTests : BaseGameTestFixture
{
    [Test]
    public async Task GetGame_Success_UnfinishedGame()
    {
        bool firstAnswer = true;
        Game game = new("gameID", "playerName", base.ValidAdventure.ID, new bool[] { firstAnswer }, false);

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(game));

        GetGameOutputModel result = await new GameService(base.HappyPathAdventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper).Get(game.ID);

        Assert.That(result, Is.Not.Null);

        Assert.That(result.GameData, Is.Not.Null);
        Assert.That(result.GameData.IsGameOver, Is.False);
        Assert.That(result.GameData.ID, Is.EqualTo(game.ID));
        Assert.That(result.GameData.AdventureID, Is.EqualTo(game.AdventureID));
        Assert.That(result.GameData.AdventureName, Is.EqualTo(base.ValidAdventure.AdventureName));

        Assert.That(result.CurrentMessage, Is.EqualTo(base.ValidAdventure.StartingNode.PositiveAnswerNode.NodeMessage));

        Assert.That(result.PreviousAnswers, Is.Not.Null);
        Assert.That(result.PreviousAnswers, Is.Not.Empty);
        Assert.That(result.PreviousAnswers, Has.Count.EqualTo(1));
        Assert.That(result.PreviousAnswers.First().ChoosenAnswer, Is.EqualTo(firstAnswer));
    }

    [Test]
    public async Task GetGame_Success_FinishedGame()
    {
        bool firstAnswer = true;
        bool secondAnswer = false;
        Game game = new("gameID", "playerName", base.ValidAdventure.ID, new bool[] { firstAnswer, secondAnswer }, true);

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(game));

        GetGameOutputModel result = await new GameService(base.HappyPathAdventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper).Get(game.ID);

        Assert.That(result, Is.Not.Null);

        Assert.That(result.GameData, Is.Not.Null);
        Assert.That(result.GameData.IsGameOver, Is.True);
        Assert.That(result.GameData.ID, Is.EqualTo(game.ID));
        Assert.That(result.GameData.AdventureID, Is.EqualTo(game.AdventureID));
        Assert.That(result.GameData.AdventureName, Is.EqualTo(base.ValidAdventure.AdventureName));

        Assert.That(result.CurrentMessage, Is.Null);

        Assert.That(result.PreviousAnswers, Is.Not.Null);
        Assert.That(result.PreviousAnswers, Is.Not.Empty);
        Assert.That(result.PreviousAnswers, Has.Count.EqualTo(3));
        Assert.That(result.PreviousAnswers.ElementAt(0).ChoosenAnswer, Is.EqualTo(firstAnswer));
        Assert.That(result.PreviousAnswers.ElementAt(1).ChoosenAnswer, Is.EqualTo(secondAnswer));
    }

    [Test]
    public void GetGame_NotExists()
    {
        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Game>(null));

        GameService gameService = new(base.HappyPathAdventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper);

        Assert.That(async () => await gameService.Get("aa"),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Game)));
    }
}