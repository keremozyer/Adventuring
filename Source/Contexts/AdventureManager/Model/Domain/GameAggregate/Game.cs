using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Domain;

namespace Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;

/// <summary>
/// Represents a game by storing the answers given in an in-order list.
/// </summary>
public class Game : IAggregateRoot
{
    /// <summary>
    /// ID of the game. Will be assigned from the data persistence layer.
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Name of the player who started the game.
    /// </summary>
    public string PlayerName { get; }
    /// <summary>
    /// ID of the adventure this game occurs in.
    /// </summary>
    public string AdventureID { get; }
    /// <summary>
    /// Previously given answers. There is no "order" property, element index is guaranteed to be in order.
    /// </summary>
    private readonly IList<NodeAnswer> Answers;
    /// <summary>
    /// Will be set to <see langword="true"/> if the game is ended.
    /// </summary>
    public bool IsGameOver { get; private set; }

    /// <summary>
    /// Constructor to use when creating a new games.
    /// Will validate the instance by calling <see cref="Validate"/> and throw the result if there are any errors.
    /// </summary>
    /// <param name="playerName">Name of the player who started the game.</param>
    /// <param name="adventureID">ID of the adventure this game occurs in.</param>
    /// <param name="firstAnswer">Answer to the initial question in the adventure.</param>
    public Game(string playerName, string adventureID, bool firstAnswer)
    {
        this.PlayerName = playerName;
        this.AdventureID = adventureID;
        this.Answers = new List<NodeAnswer>() { new(firstAnswer) };

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }
    }

    /// <summary>
    /// Constructor to use when reading an existing AdventureTree to service layer.
    /// </summary>
    /// <param name="id">ID of the game. Will be assigned from the data persistence layer.</param>
    /// <param name="playerName">Name of the player who started the game.</param>
    /// <param name="adventureID">ID of the adventure this game occurs in.</param>
    /// <param name="answers">Previously given answers. There is no "order" property, element index is guaranteed to be in order.</param>
    /// <param name="isGameOver">Should be <see langword="true"/> if the game is ended.</param>
    public Game(string id, string playerName, string adventureID, IEnumerable<bool> answers, bool isGameOver)
    {
        this.ID = id;
        this.PlayerName = playerName;
        this.AdventureID = adventureID;
        this.Answers = answers.Select(answer => new NodeAnswer(answer)).ToList();
        this.IsGameOver = isGameOver;
    }

    /// <summary>
    /// Returns the previously given answers in the game.
    /// </summary>
    /// <returns></returns>
    public IList<NodeAnswer> GetAnswers()
    {
        return this.Answers;
    }

    /// <summary>
    /// Advances the game to the next layer in the path specified in <paramref name="answer"/> parameter.
    /// </summary>
    /// <param name="answer">Path to take.</param>
    public void AdvanceGame(bool answer)
    {
        this.Answers.Add(new(answer));
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame()
    {
        this.IsGameOver = true;
    }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public ValidationException? Validate()
    {
        List<ValidationExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.PlayerName))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.PlayerName)));
        }

        if (String.IsNullOrWhiteSpace(this.AdventureID))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.AdventureID)));
        }

        if (this.Answers.IsNullOrEmpty())
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.Answers)));
        }

        return errors is null ? null : new ValidationException(errors);
    }
}