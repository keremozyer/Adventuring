using Newtonsoft.Json;

namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Contains extension methods for all objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Serializes given object as a JSON string. If the object is <see langword="null"/> the result will also be <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">Object to serialize.</param>
    /// <returns>JSON string.</returns>
    public static string SerializeAsJson<T>(this T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        });
    }

    /// <summary>
    /// Creates a deep copy of the <paramref name="obj"/>. The new object won't share any reference with the original one and will occupy a new place in the memory. If the original value is <see langword="null"/> the result will also be <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">Object to copy.</param>
    /// <returns>A copy of the <paramref name="obj"/>.</returns>
    public static T DeepCopy<T>(this T obj)
    {
        return obj == null ? obj : obj.SerializeAsJson().DeserializeJSON<T>();
    }
}