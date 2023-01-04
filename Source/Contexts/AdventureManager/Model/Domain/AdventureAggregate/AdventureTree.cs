using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Domain;

namespace Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;

/// <summary>
/// Represents the adventure as a binary choice decision tree with <see cref="StartingNode"/> as the entry point.
/// </summary>
public class AdventureTree : IAggregateRoot
{
    /// <summary>
    /// ID of the adventure. Will be assigned from the data persistence layer.
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public string AdventureName { get; }
    /// <summary>
    /// Entry point for the adventure.
    /// </summary>
    public AdventureNode StartingNode { get; set; }

    /// <summary>
    /// Constructor to use when creating new adventures.
    /// Will validate the instance by calling <see cref="Validate"/> and throw the result if there are any errors.
    /// </summary>
    /// <param name="adventureName">Name of the adventure.</param>
    /// <param name="startingNodeMessage">First question in the adventure. Will be assigned to <see cref="StartingNode"/> property.</param>
    public AdventureTree(string adventureName, string startingNodeMessage)
    {
        this.AdventureName = adventureName;
        this.StartingNode = new AdventureNode(startingNodeMessage);

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }
    }

    /// <summary>
    /// Constructor to use when reading an existing AdventureTree to service layer.
    /// Will validate the instance by calling <see cref="Validate"/> and throw the result if there are any errors.
    /// </summary>
    /// <param name="id">ID of the adventure. Will be assigned from the data persistence layer.</param>
    /// <param name="adventureName">Name of the adventure.</param>
    /// <param name="startingNodeMessage">First question in the adventure. Will be assigned to <see cref="StartingNode"/> property.</param>
    public AdventureTree(string id, string adventureName, string startingNodeMessage)
    {
        this.ID = id;
        this.AdventureName = adventureName;
        this.StartingNode = new AdventureNode(startingNodeMessage);

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }
    }

    /// <summary>
    /// Finds the node reached when following the path in <paramref name="answers"/>.
    /// </summary>
    /// <param name="answers">Path to follow.</param>
    /// <returns>Will return the message of the found node and an indicator showing whether it's an end node or not.</returns>
    public (string Message, bool IsEndNode) GetTargetNodeMessage(IEnumerable<bool> answers)
    {
        AdventureNode currentNode = this.StartingNode;
        foreach (bool answer in answers)
        {
            currentNode = answer ? currentNode.PositiveAnswerNode! : currentNode.NegativeAnswerNode!;
        }

        return (currentNode.NodeMessage, currentNode.PositiveAnswerNode is null);
    }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public ValidationException? Validate()
    {
        List<ValidationExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.AdventureName))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.AdventureName)));
        }

        errors = errors.AddRangeSafe(ValidateNode(this.StartingNode));

        return errors is null ? null : new ValidationException(errors);

        static IEnumerable<ValidationExceptionMessage>? ValidateNode(AdventureNode? node)
        {
            if (node is null)
            {
                return null;
            }

            List<ValidationExceptionMessage>? errors = null;

            errors = errors.AddRangeSafe(node.Validate()?.Messages?.Cast<ValidationExceptionMessage>());

            return errors;
        }
    }
}