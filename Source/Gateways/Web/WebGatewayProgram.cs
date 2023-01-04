using Adventuring.Architecture.Application.Gateway.Core;

namespace Adventuring.Gateways.Web;

public class WebGatewayProgram : BaseGatewayProgram
{
    public WebGatewayProgram(string[] arguments) : base(GetConfigurationOptions(arguments)) { }

    private static GatewayProgramConfigurationOptions GetConfigurationOptions(string[] arguments)
    {
        return new GatewayProgramConfigurationOptions(arguments)
        {
        };
    }
}