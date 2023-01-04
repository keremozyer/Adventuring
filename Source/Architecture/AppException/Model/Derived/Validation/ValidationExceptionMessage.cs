using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.Validation;

/// <summary></summary>
public class ValidationExceptionMessage : BaseAppExceptionMessage
{
    /// <summary>
    /// Name of the validated field.
    /// </summary>
    public string FieldName { get; }
    /// <summary>
    /// Any extra data to include in the error message.
    /// </summary>
    public IReadOnlyCollection<string> ExtraInfo { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <param name="fieldName">Name of the validated field.</param>
    /// <param name="extraInfo">Any extra data to include in the error message.</param>
    public ValidationExceptionMessage(ErrorCodes code, string fieldName, params string[] extraInfo) : base(code, null)
    {
        this.FieldName = fieldName;
        this.ExtraInfo = extraInfo;
    }
}