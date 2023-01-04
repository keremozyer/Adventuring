using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;

/// <summary>
/// Exception type to use when a preconfigured reference data is invalid.
/// </summary>
public class InvalidReferenceDataException : BaseAppException
{
    /// <summary></summary>
    /// <param name="messages"></param>
    public InvalidReferenceDataException(IReadOnlyCollection<InvalidReferenceDataExceptionMessage> messages) : base(messages) { }

    /// <summary>
    /// Will create a new <see cref="InvalidReferenceDataExceptionMessage"/> with <paramref name="code"/> and <paramref name="referenceDataName"/> and store it in the instance.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="referenceDataName"></param>
    public InvalidReferenceDataException(ErrorCodes code, string referenceDataName) : base(GetMessage(code, referenceDataName)) { }

    private static IReadOnlyCollection<BaseAppExceptionMessage> GetMessage(ErrorCodes code, string referenceDataName)
    {
        return new InvalidReferenceDataExceptionMessage[]
        {
            new(code, referenceDataName)
        };
    }
}