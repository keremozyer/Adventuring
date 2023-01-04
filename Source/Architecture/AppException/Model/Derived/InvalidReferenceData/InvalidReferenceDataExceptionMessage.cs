using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;

/// <summary></summary>
public class InvalidReferenceDataExceptionMessage : BaseAppExceptionMessage
{
    /// <summary>
    /// Name of the invalid reference data configuration.
    /// </summary>
    public string ReferenceDataName { get; }

    /// <summary></summary>
    /// <param name="code">Error code.</param>
    /// <param name="referenceDataName">Name of the invalid reference data configuration.</param>
    public InvalidReferenceDataExceptionMessage(ErrorCodes code, string referenceDataName) : base(code, null)
    {
        this.ReferenceDataName = referenceDataName;
    }
}