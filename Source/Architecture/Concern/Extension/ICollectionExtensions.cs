namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Contains extension methods for ICollection interface.
/// </summary>
public static class ICollectionExtensions
{
    /// <summary>
    /// Removes the element specified by <paramref name="predicate"/> from the collection if it exists. Does nothing if element does not exists.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="collection"></param>
    /// <param name="predicate"></param>
    public static void RemoveBy<ElementType>(this ICollection<ElementType> collection, Func<ElementType, bool> predicate)
    {
        ElementType? element = collection.FirstOrDefault(predicate);
        if (element is null)
        {
            return;
        }

        _ = collection.Remove(element);
    }
}