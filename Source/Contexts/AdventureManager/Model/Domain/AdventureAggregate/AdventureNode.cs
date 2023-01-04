using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Domain;

namespace Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;

/// <summary>
/// A node in the adventure tree.
/// </summary>
public class AdventureNode : IDomainObject
{
    /// <summary>
    /// Message of the node.
    /// </summary>
    public string NodeMessage { get; }
    /// <summary>
    /// Next node in the positive path.
    /// </summary>
    public AdventureNode? PositiveAnswerNode { get; private set; }
    /// <summary>
    /// Next node in the negative path.
    /// </summary>
    public AdventureNode? NegativeAnswerNode { get; private set; }

    /// <summary>
    /// Will validate the instance by calling <see cref="Validate"/> and throw the result if there are any errors.
    /// </summary>
    /// <param name="nodeMessage">Message of the node.</param>
    public AdventureNode(string nodeMessage)
    {
        this.NodeMessage = nodeMessage;

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }
    }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public ValidationException? Validate()
    {
        List<ValidationExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.NodeMessage))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.NodeMessage)));
        }

        errors = errors.AddSafe(ValidateNodeBalance(this.PositiveAnswerNode, this.NegativeAnswerNode, nameof(this.NegativeAnswerNode), this.NodeMessage));
        errors = errors.AddSafe(ValidateNodeBalance(this.NegativeAnswerNode, this.PositiveAnswerNode, nameof(this.PositiveAnswerNode), this.NodeMessage));
        
        errors = errors.AddRangeSafe(this.PositiveAnswerNode?.Validate()?.Messages?.Cast<ValidationExceptionMessage>());
        errors = errors.AddRangeSafe(this.NegativeAnswerNode?.Validate()?.Messages?.Cast<ValidationExceptionMessage>());

        return errors is null ? null : new ValidationException(errors);

        static ValidationExceptionMessage? ValidateNodeBalance(AdventureNode? existingNode, AdventureNode? nonExistingNode, string balanceBreakingNodeName, string currentNodeMessage)
        {
            // If a node is not an end node it must both have a negative path and a positive path.
            return existingNode is not null && nonExistingNode is null ? new ValidationExceptionMessage(ErrorCodes.NodeMustBeBalanced, currentNodeMessage, balanceBreakingNodeName) : null;
        }
    }

    /// <summary>
    /// Adds the given <paramref name="nodeMessage"/> as the next node of this node's positive path.
    /// </summary>
    /// <param name="nodeMessage">Next node's message.</param>
    public void AddPositiveAnswerNode(string nodeMessage)
    {
        this.PositiveAnswerNode = new AdventureNode(nodeMessage);
    }

    /// <summary>
    /// Adds the given <paramref name="nodeMessage"/> as the next node of this node's negative path.
    /// </summary>
    /// <param name="nodeMessage">Next node's message.</param>
    public void AddNegativeAnswerNode(string nodeMessage)
    {
        this.NegativeAnswerNode = new AdventureNode(nodeMessage);
    }
}