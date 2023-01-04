namespace Adventuring.Architecture.Data.Context.Interface;

/// <summary></summary>
public interface IDataContext
{
    /// <summary>
    /// Commits the ongoing transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task Save(CancellationToken cancellationToken = default);
}