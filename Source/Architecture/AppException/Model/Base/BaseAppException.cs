namespace Adventuring.Architecture.AppException.Model.Base;

/// <summary>
/// All custom defined application exceptions must be derived from this class.
/// </summary>
public class BaseAppException : Exception
{
    /// <summary>
    /// Error messages.
    /// </summary>
    public IReadOnlyCollection<BaseAppExceptionMessage> Messages { get; }

    /// <summary></summary>
    /// <param name="messages">Error messages.</param>
    public BaseAppException(IReadOnlyCollection<BaseAppExceptionMessage> messages)
    {
        this.Messages = messages;
    }
}