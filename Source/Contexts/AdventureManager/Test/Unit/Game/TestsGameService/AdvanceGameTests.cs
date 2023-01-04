using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game.TestsGameService;

public class AdvanceGameTests : BaseGameTestFixture
{
    [Test]
    public async Task AdvanceGame_Success_FinishingGame()
    {
        bool firstAnswer = true;
        bool secondAnswer = false;
        Game game = new("gameID", "playerName", base.ValidAdventure.ID, new bool[] { firstAnswer }, false);

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(game));

        Mock<IAdventureTreeService> adventureTreeServiceMock = new();
        _ = adventureTreeServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(this.ValidAdventure));
        _ = adventureTreeServiceMock.Setup(x => x.GetTargetNodeMessage(It.IsAny<string>(), It.IsAny<IEnumerable<bool>>())).Returns(Task.FromResult(("mes", true)));

        AdvanceGameOutputModel result = await new GameService(adventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper).AdvanceGame(new AdvanceGameInputModel() { GameID = game.ID, ChoosenPath = secondAnswer });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.NextMessage, Is.Not.Null);
        Assert.That(result.NextMessage, Is.Not.Empty);
        Assert.That(result.IsEndNode, Is.True);
    }

    [Test]
    public async Task AdvanceGame_Success_NotFinishingGame()
    {
        bool firstAnswer = true;
        bool secondAnswer = true;
        Game game = new("gameID", "playerName", base.ValidAdventure.ID, new bool[] { firstAnswer }, false);

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(game));

        Mock<IAdventureTreeService> adventureTreeServiceMock = new();
        _ = adventureTreeServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(this.ValidAdventure));
        _ = adventureTreeServiceMock.Setup(x => x.GetTargetNodeMessage(It.IsAny<string>(), It.IsAny<IEnumerable<bool>>())).Returns(Task.FromResult(("mes", false)));

        AdvanceGameOutputModel result = await new GameService(adventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper).AdvanceGame(new AdvanceGameInputModel() { GameID = game.ID, ChoosenPath = secondAnswer });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.NextMessage, Is.Not.Null);
        Assert.That(result.NextMessage, Is.Not.Empty);
        Assert.That(result.IsEndNode, Is.False);
    }

    [Test]
    public void AdvanceGame_Game_Not_Exists()
    {
        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Game>(null));

        GameService gameService = new(base.HappyPathAdventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper);

        Assert.That(async () => await gameService.AdvanceGame(new AdvanceGameInputModel() { GameID = "a", ChoosenPath = true }),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Game)));
    }

    [Test]
    public void AdvanceGame_Game_AlreadyFinished()
    {
        bool firstAnswer = true;
        bool secondAnswer = false;
        Game game = new("gameID", "playerName", base.ValidAdventure.ID, new bool[] { firstAnswer, secondAnswer }, true);

        Mock<IGameDataStore> mockGameDataStore = new();
        _ = mockGameDataStore.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(game));

        GameService gameService = new(base.HappyPathAdventureTreeServiceMock.Object, base.HappyPathActiveUserMock.Object, mockGameDataStore.Object, base.GameMapper);

        Assert.That(async () => await gameService.AdvanceGame(new AdvanceGameInputModel() { GameID = "a", ChoosenPath = true }),
            Throws.TypeOf<BusinessException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(BaseAppExceptionMessage.Code)).EqualTo(ErrorCodes.GameIsAlreadyOver));
    }
}