using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;

/// <summary>
/// Exception type to use when the user tries to perform an operation they are not authorized to.
/// </summary>
public class UnauthorizedOperationException : BaseAppException
{
    /// <summary>
    /// Will create a new <see cref="UnauthorizedOperationExceptionMessage"/> with <paramref name="operationName"/> and store it in the instance.
    /// </summary>
    /// <param name="operationName"></param>
    public UnauthorizedOperationException(string operationName) : base(GetMessage(operationName)) { }

    private static IReadOnlyCollection<UnauthorizedOperationExceptionMessage> GetMessage(string operationName)
    {
        return new UnauthorizedOperationExceptionMessage[]
        {
            new(operationName)
        };
    }
}