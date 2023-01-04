using Microsoft.Extensions.Logging;

namespace Adventuring.Architecture.Concern.Constant.Logging;

/// <summary>
/// Event IDs to use when creating logs.
/// </summary>
public static class LogEvents
{
    /// <summary>
    /// EventId to use when a template is not found for an error code.
    /// </summary>
    public static readonly EventId ExceptionMessageTemplateNotFound = new(101, nameof(ExceptionMessageTemplateNotFound));
}