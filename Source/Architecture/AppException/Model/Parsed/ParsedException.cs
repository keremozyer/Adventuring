using System.Net;

namespace Adventuring.Architecture.AppException.Model.Parsed;

/// <summary>
/// A standard error model to represent parsed (to a user friendly format) errors.
/// </summary>
public class ParsedException
{
    /// <summary>
    /// <see cref="HttpStatusCode"/> corrosponding to this error.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
    /// <summary>
    /// If this is a logged exception log's ID will be present here.
    /// </summary>
    public string? LogID { get; set; }
    /// <summary>
    /// User friendly error messages.
    /// </summary>
    public IEnumerable<ParsedExceptionMessage>? Messages { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="logID"></param>
    /// <param name="messages"></param>
    public ParsedException(HttpStatusCode statusCode, string? logID, params ParsedExceptionMessage[]? messages)
    {
        this.StatusCode = statusCode;
        this.LogID = logID;
        this.Messages = messages;
    }
}