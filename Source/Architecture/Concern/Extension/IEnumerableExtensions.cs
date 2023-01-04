namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Contains extension methods for IEnumerable interface.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Returns <see langword="true"/> if <paramref name="collection"/> contains elements. Returns <see langword="false"/> if <paramref name="collection"/> is <see langword="null"/> or empty.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool HasElements<ElementType>(this IEnumerable<ElementType> collection)
    {
        return collection is not null && collection.Any();
    }

    /// <summary>
    /// Returns <see langword="true"/> if <paramref name="collection"/> is <see langword="null"/> or has no elements. Otherwise returns <see langword="false"/>.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<ElementType>(this IEnumerable<ElementType> collection)
    {
        return !HasElements(collection);
    }

    /// <summary>
    /// Intersects <paramref name="firstCollection"/> and <paramref name="secondCollection"/> in a safe manner.
    /// If any of them is <see langword="null"/> or empty <see langword="null"/> will be returned.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="firstCollection"></param>
    /// <param name="secondCollection"></param>
    /// <returns></returns>
    public static IEnumerable<ElementType>? IntersectSafe<ElementType>(this IEnumerable<ElementType>? firstCollection, IEnumerable<ElementType>? secondCollection)
    {
        return firstCollection!.IsNullOrEmpty() || secondCollection!.IsNullOrEmpty() ? null : firstCollection!.Intersect(secondCollection!);
    }

    /// <summary>
    /// Concats the <paramref name="firstEnumerable"/> with <paramref name="secondEnumerable"/> in a safe manner.
    /// If <paramref name="firstEnumerable"/> is <see langword="null"/> the <paramref name="secondEnumerable"/> will be returned.
    /// If <paramref name="secondEnumerable"/> is <see langword="null"/> the <paramref name="firstEnumerable"/> will be returned.
    /// If they are both <see langword="null"/>, <see langword="null"/> will be returned.
    /// </summary>
    /// <typeparam name="ElementType"></typeparam>
    /// <param name="firstEnumerable"></param>
    /// <param name="secondEnumerable"></param>
    /// <returns></returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Causes triple nested ternary operators.")]
    public static IEnumerable<ElementType>? ConcatSafe<ElementType>(this IEnumerable<ElementType>? firstEnumerable, IEnumerable<ElementType>? secondEnumerable)
    {
        if (firstEnumerable!.IsNullOrEmpty() && secondEnumerable!.IsNullOrEmpty())
        {
            return null;
        }

        if (firstEnumerable!.IsNullOrEmpty())
        {
            return secondEnumerable;
        }

        if (secondEnumerable!.IsNullOrEmpty())
        {
            return firstEnumerable;
        }

        return firstEnumerable!.Concat(secondEnumerable!);
    }
}