using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.DataNotFound;

/// <summary>
/// Exception type to use when a data record is not found in the application's data store and is required for the current operation to continue.
/// </summary>
public class DataNotFoundException : BaseAppException
{
    /// <summary></summary>
    /// <param name="messages"></param>
    public DataNotFoundException(IReadOnlyCollection<DataNotFoundExceptionMessage> messages) : base(messages) { }

    /// <summary>
    /// Will create a new <see cref="DataNotFoundExceptionMessage"/> with <see cref="ErrorCodes.DataNotFound"/> error code, <paramref name="searchedEntity"/> and <paramref name="searchValue"/> and store it in the instance.
    /// </summary>
    /// <param name="searchedEntity"></param>
    /// <param name="searchValue"></param>
    public DataNotFoundException(string searchedEntity, string searchValue) : base(GetMessage(searchedEntity, searchValue)) { }

    private static IReadOnlyCollection<BaseAppExceptionMessage> GetMessage(string searchedEntity, string searchValue)
    {
        return new DataNotFoundExceptionMessage[]
        {
            new(ErrorCodes.DataNotFound, searchedEntity, searchValue)
        };
    }
}