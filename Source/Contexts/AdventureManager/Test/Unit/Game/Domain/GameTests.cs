using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game.Domain;

public class GameTests : BaseGameTestFixture
{
    [Test]
    public void Game_Create_Success()
    {
        string playerName = "a";
        string adventureID = "b";
        bool firstAnswer = true;

        Game game = new(playerName, adventureID, firstAnswer);

        Assert.Multiple(() =>
        {
            Assert.That(game.PlayerName, Is.EqualTo(playerName));
            Assert.That(game.AdventureID, Is.EqualTo(adventureID));
        });

        IList<NodeAnswer> answers = game.GetAnswers();

        Assert.That(answers, Is.Not.Null);
        Assert.That(answers, Is.Not.Empty);
        Assert.That(answers, Has.Count.EqualTo(1));
        Assert.That(answers.First().ChoosenPath, Is.EqualTo(firstAnswer));
    }

    [Test]
    public void Game_Create_PlayerName_Null()
    {
        Assert.That(() => new Game(null, "a", true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.PlayerName)));
    }

    [Test]
    public void Game_Create_PlayerName_Empty()
    {
        Assert.That(() => new Game("", "a", true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.PlayerName)));
    }

    [Test]
    public void Game_Create_PlayerName_Whitespace()
    {
        Assert.That(() => new Game("  ", "a", true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.PlayerName)));
    }

    [Test]
    public void Game_Create_AdventureID_Null()
    {
        Assert.That(() => new Game("a", null, true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.AdventureID)));
    }

    [Test]
    public void Game_Create_AdventureID_Empty()
    {
        Assert.That(() => new Game("a", "", true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.AdventureID)));
    }

    [Test]
    public void Game_Create_AdventureID_Whitespace()
    {
        Assert.That(() => new Game("a", "  ", true),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Game.AdventureID)));
    }

    [Test]
    public void Game_AdvanceGame_Positive()
    {
        Game game = new("a", "b", true);
        game.AdvanceGame(true);
        IList<NodeAnswer> answers = game.GetAnswers();

        Assert.That(answers, Is.Not.Null);
        Assert.That(answers, Is.Not.Empty);
        Assert.That(answers, Has.Count.EqualTo(2));
        Assert.That(answers[0].ChoosenPath, Is.True);
        Assert.That(answers[1].ChoosenPath, Is.True);
    }

    [Test]
    public void Game_AdvanceGame_Negative()
    {
        Game game = new("a", "b", true);
        game.AdvanceGame(false);
        IList<NodeAnswer> answers = game.GetAnswers();

        Assert.That(answers, Is.Not.Null);
        Assert.That(answers, Is.Not.Empty);
        Assert.That(answers, Has.Count.EqualTo(2));
        Assert.That(answers[0].ChoosenPath, Is.True);
        Assert.That(answers[1].ChoosenPath, Is.False);
    }

    [Test]
    public void Game_EndGame()
    {
        Game game = new("a", "b", true);
        Assert.That(game.IsGameOver, Is.False);

        game.EndGame();
        Assert.That(game.IsGameOver, Is.True);
    }
}