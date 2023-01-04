using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Model.Interface.Domain;

namespace Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;

/// <summary>
/// Represents a previous answer given in a game.
/// </summary>
public class NodeAnswer : IDomainObject
{
    /// <summary>
    /// Choosen answer for the node.
    /// </summary>
    public bool ChoosenPath { get; }

    /// <summary></summary>
    /// <param name="choosenPath">Choosen answer for the node.</param>
    public NodeAnswer(bool choosenPath)
    {
        this.ChoosenPath = choosenPath;
    }

    /// <summary>
    /// Does nothing since this model does not requires validation.
    /// </summary>
    /// <returns></returns>
    public ValidationException? Validate()
    {
        return null;
    }
}