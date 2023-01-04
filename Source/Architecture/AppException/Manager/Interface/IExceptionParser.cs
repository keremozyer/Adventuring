using Adventuring.Architecture.AppException.Model.Parsed;

namespace Adventuring.Architecture.AppException.Manager.Interface;

/// <summary>
/// Interface to parse handled and unhandled exceptions to user-friendly format.
/// </summary>
public interface IExceptionParser
{
    /// <summary>
    /// </summary>
    /// <param name="exception">Exception to parse.</param>
    /// <returns>A user-friendly error model.</returns>
    public ParsedException Parse(Exception exception);
}