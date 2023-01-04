using Adventuring.Architecture.Model.Entity.Interface;

namespace Adventuring.Architecture.Model.Entity.Implementation.Record;

/// <summary>
/// All hard deleted records must be derived from this class.
/// </summary>
public abstract class BaseHardDeletedRecord : BaseRecord, IHardDeletedEntity
{

}