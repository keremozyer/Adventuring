using Adventuring.Architecture.AppException.Concern.Constant;

namespace Adventuring.Architecture.AppException.Model.Base;

/// <summary>
/// All custom defined application exception messages must be derived from this class.
/// </summary>
public abstract class BaseAppExceptionMessage
{
    /// <summary>
    /// Error code.
    /// </summary>
    public ErrorCodes Code { get; }
    /// <summary>
    /// Error message.
    /// </summary>
    public string? Message { get; }

    /// <summary></summary>
    /// <param name="code">Error code.</param>
    /// <param name="message">Error message.</param>
    public BaseAppExceptionMessage(ErrorCodes code, string? message = null)
    {
        this.Code = code;
        this.Message = message;
    }
}