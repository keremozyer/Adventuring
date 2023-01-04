using Adventuring.Architecture.AppException.Concern.Constant;

namespace Adventuring.Architecture.AppException.Model.Parsed;

/// <summary>
/// A standard error model to represent error codes and user friendly messages.
/// </summary>
public class ParsedExceptionMessage
{
    /// <summary>
    /// Error's code.
    /// </summary>
    public ErrorCodes Code { get; set; }
    /// <summary>
    /// User friendly error message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Initializes a new exception message with given <paramref name="code"/> and <paramref name="message"/>.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public ParsedExceptionMessage(ErrorCodes code, string message)
    {
        this.Code = code;
        this.Message = message;
    }
}