using Adventuring.Architecture.Model.Interface.ServiceTypes;

namespace Adventuring.Architecture.Application.Gateway.Concern.Option.HealthCheck;

public class HealthCheckSettings : IOptionService
{
    public HealthCheckModel[]? HealthChecks { get; set; }
}