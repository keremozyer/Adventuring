using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;

/// <summary></summary>
public class UnauthorizedOperationExceptionMessage : BaseAppExceptionMessage
{
    /// <summary>
    /// Name of the unauthorized operation.
    /// </summary>
    public string OperationName { get; set; }

    /// <summary>
    /// Creates a new UnauthorizedOperationExceptionMessage with <see cref="ErrorCodes.UnauthorizedOperation"/> error code.
    /// </summary>
    /// <param name="operationName">Name of the unauthorized operation.</param>
    public UnauthorizedOperationExceptionMessage(string operationName) : base(ErrorCodes.UnauthorizedOperation)
    {
        this.OperationName = operationName;
    }
}