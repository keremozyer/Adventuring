using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.Business;

/// <summary>
/// Exception type to use when business errors happen.
/// </summary>
public class BusinessException : BaseAppException
{
    /// <summary>
    /// Will create a new <see cref="BusinessExceptionMessage"/> with <paramref name="errorCode"/> and store it in the instance.
    /// </summary>
    /// <param name="errorCode"></param>
    public BusinessException(ErrorCodes errorCode) : base(GetMessage(errorCode)) { }

    private static IReadOnlyCollection<BaseAppExceptionMessage> GetMessage(ErrorCodes errorCode)
    {
        return new BusinessExceptionMessage[]
        {
            new(errorCode)
        };
    }
}