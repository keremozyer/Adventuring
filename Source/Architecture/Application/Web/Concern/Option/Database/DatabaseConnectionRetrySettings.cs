namespace Adventuring.Architecture.Application.Web.Concern.Option.Database;

public class DatabaseConnectionRetrySettings
{
    public int? Retries { get; set; }
    public int? IntervalInMilliseconds { get; set; }
}