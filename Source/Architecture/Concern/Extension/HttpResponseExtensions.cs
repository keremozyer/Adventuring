using Microsoft.AspNetCore.Http;

namespace Adventuring.Architecture.Concern.Extension;

/// <summary>
/// Extension methods for HttpResponse class.
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Returns <see langword="true"/> if StatusCode is in range of [200, 299] and <see langword="false"/> otherwise.
    /// </summary>
    /// <param name="httpResponse"></param>
    /// <returns></returns>
    public static bool IsSuccessful(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode is >= 200 and <= 299;
    }
}