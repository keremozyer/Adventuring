using Newtonsoft.Json;

namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Contains extension methods for the <see cref="String"/> class.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Deserializes the source string to the given <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the target object.</typeparam>
    /// <param name="text">Source string.</param>
    /// <returns>A new instance of <typeparamref name="T"/>.</returns>
    public static T DeserializeJSON<T>(this string text)
    {
        return JsonConvert.DeserializeObject<T>(text)!;
    }
}