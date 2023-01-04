namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Extension methods for <see cref="List{T}"/> class.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Adds the given <paramref name="elementToAdd"/> to the <paramref name="collection"/> in a safe manner.
    /// If <paramref name="elementToAdd"/> is <see langword="null"/> this method does nothing.
    /// If <paramref name="collection"/> is <see langword="null"/> this method first initializes a new <see cref="List{ElementType}"/> and then adds the element to it.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="collection">Collection to add the <paramref name="elementToAdd"/> into.</param>
    /// <param name="elementToAdd">Element to add in <paramref name="collection"/>.</param>
    /// <returns>Modified collection. If <paramref name="collection"/> is suspected to be <see langword="null"/> the return value must be used.</returns>
    public static List<ElementType>? AddSafe<ElementType>(this List<ElementType>? collection, ElementType? elementToAdd)
    {
        if (elementToAdd is not null)
        {
            collection ??= new List<ElementType>();

            collection.Add(elementToAdd);
        }

        return collection;
    }

    /// <summary>
    /// Adds given <paramref name="elementsToAdd"/> to the <paramref name="collection"/> in a safe manner.
    /// If <paramref name="elementsToAdd"/> is <see langword="null"/> this method does nothing.
    /// If <paramref name="collection"/> is <see langword="null"/> this method first initializes a new <see cref="List{ElementType}"/> and then adds elements to it.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="collection">Collection to add the <paramref name="elementsToAdd"/> into.</param>
    /// <param name="elementsToAdd">Elements to add in <paramref name="collection"/>.</param>
    /// <returns>Modified collection. If <paramref name="collection"/> is suspected to be <see langword="null"/> the return value must be used.</returns>
    public static List<ElementType>? AddRangeSafe<ElementType>(this List<ElementType>? collection, IEnumerable<ElementType>? elementsToAdd)
    {
        if (elementsToAdd is not null)
        {
            collection ??= new List<ElementType>();

            collection.AddRange(elementsToAdd);
        }

        return collection;
    }
}