using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;

namespace Adventuring.Architecture.AppException.Model.Derived.DataNotFound;

/// <summary></summary>
public class DataNotFoundExceptionMessage : BaseAppExceptionMessage
{
    /// <summary>
    /// Searched entity's name.
    /// </summary>
    public string SearchedEntity { get; }
    /// <summary>
    /// Value used in search.
    /// </summary>
    public string SearchValue { get; }

    /// <summary></summary>
    /// <param name="code">Error code.</param>
    /// <param name="searchedEntity">Searched entity's name.</param>
    /// <param name="searchValue">Value used in search.</param>
    public DataNotFoundExceptionMessage(ErrorCodes code, string searchedEntity, string searchValue) : base(code, null)
    {
        this.SearchedEntity = searchedEntity;
        this.SearchValue = searchValue;
    }

    /// <summary>
    /// Creates a new DataNotFoundExceptionMessage with <see cref="ErrorCodes.DataNotFound"/> as error code.
    /// </summary>
    /// <param name="searchedEntity">Searched entity's name.</param>
    /// <param name="searchValue">Value used in search.</param>
    public DataNotFoundExceptionMessage(string searchedEntity, string searchValue) : base(ErrorCodes.DataNotFound, null)
    {
        this.SearchedEntity = searchedEntity;
        this.SearchValue = searchValue;
    }
}