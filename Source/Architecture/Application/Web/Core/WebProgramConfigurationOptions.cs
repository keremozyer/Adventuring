using AutoMapper;

namespace Adventuring.Architecture.Application.Web.Core;

/// <summary>
/// Options to configure web applications.
/// </summary>
public class WebProgramConfigurationOptions
{
    /// <summary>
    /// Application start arguments.
    /// </summary>
    public string[] Arguments { get; set; }
    /// <summary>
    /// AutoMapper profile if the application uses it.
    /// </summary>
    public Profile? AutoMapperProfile { get; set; }

    /// <summary></summary>
    /// <param name="arguments">Application start arguments.</param>
    public WebProgramConfigurationOptions(string[] arguments)
    {
        this.Arguments = arguments;
    }
}