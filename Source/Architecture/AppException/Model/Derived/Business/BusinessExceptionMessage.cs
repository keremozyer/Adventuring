using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.Business;

/// <summary></summary>
public class BusinessExceptionMessage : BaseAppExceptionMessage
{
    /// <summary></summary>
    /// <param name="code">Error code.</param>
    public BusinessExceptionMessage(ErrorCodes code) : base(code, null) { }
}