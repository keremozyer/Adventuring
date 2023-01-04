namespace Adventuring.Architecture.Concern.Attribute.TransactionHandling;

/// <summary>
/// Marks a controller or action as transactional operation. Used to force the system to use transactions even in HttpMethods where transactions are not started as default.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TransactionalOperationAttribute : System.Attribute
{

}