using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.Validation;

/// <summary>
/// Exception type to use when validation errors happen.
/// </summary>
public class ValidationException : BaseAppException
{
    /// <summary></summary>
    /// <param name="messages"></param>
    public ValidationException(IReadOnlyCollection<ValidationExceptionMessage> messages) : base(messages) { }

    /// <summary>
    /// Will create a new <see cref="ValidationExceptionMessage"/> with <paramref name="errorCode"/> and <paramref name="fieldName"/> and store it in the instance.
    /// </summary>
    /// <param name="errorCode">Error code.</param>
    /// <param name="fieldName">Name of the validated field.</param>
    public ValidationException(ErrorCodes errorCode, string fieldName) : base(GetMessage(errorCode, fieldName)) { }

    private static IReadOnlyCollection<ValidationExceptionMessage> GetMessage(ErrorCodes errorCode, string fieldName)
    {
        return new ValidationExceptionMessage[]
        {
            new(errorCode, fieldName)
        };
    }
}