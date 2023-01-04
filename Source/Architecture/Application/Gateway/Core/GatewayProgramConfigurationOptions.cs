using Adventuring.Architecture.Application.Web.Core;

namespace Adventuring.Architecture.Application.Gateway.Core;

/// <summary>
/// Options to configure Gateway programs.
/// </summary>
public class GatewayProgramConfigurationOptions : WebProgramConfigurationOptions
{
    /// <summary></summary>
    /// <param name="arguments">Application start arguments.</param>
    public GatewayProgramConfigurationOptions(string[] arguments) : base(arguments) { }
}