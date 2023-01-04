using Adventuring.Architecture.Application.Gateway.Concern.Option.HealthCheck;
using Adventuring.Architecture.Application.Web.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Adventuring.Architecture.Application.Gateway.Core;

/// <summary>
/// Gateway initializer.
/// </summary>
public abstract class BaseGatewayProgram : BaseWebProgram
{
    private readonly string CorsPolicy = nameof(CorsPolicy);

    /// <summary></summary>
    /// <param name="webProgramConfigurationOptions"></param>
    protected BaseGatewayProgram(WebProgramConfigurationOptions webProgramConfigurationOptions) : base(webProgramConfigurationOptions)
    {
        RunHealthChecks();
    }

    private void RunHealthChecks()
    {
        _ = Parallel.ForEach(base.WebApplication.Configuration.GetSection(nameof(HealthCheckSettings)).Get<HealthCheckSettings>()?.HealthChecks ?? Array.Empty<HealthCheckModel>(), healthCheckModel =>
        {
            int currentRunNumber = 0;
            do
            {
                try
                {
                    currentRunNumber++;
                    _ = new HttpClient().GetAsync(healthCheckModel.URL).Result.EnsureSuccessStatusCode();
                    break;
                }
                catch (Exception)
                {
                    if (currentRunNumber < healthCheckModel.Retries!.Value)
                    {
                        Thread.Sleep(healthCheckModel.IntervalInMilliseconds!.Value);
                    }
                }
            } while (currentRunNumber <= healthCheckModel.Retries!.Value);
        });
    }

    /// <inheritdoc/>
    protected override void ConfigureServices()
    {
        _ = base.WebApplicationBuilder.Services.AddCors(options => options.AddPolicy(this.CorsPolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        _ = base.WebApplicationBuilder.Services.AddSwaggerForOcelot(base.WebApplicationBuilder.Configuration).AddOcelot();
    }

    /// <inheritdoc/>
    protected override void ConfigureWebApplication()
    {
        _ = base.WebApplication
            .UseHttpsRedirection()
            .UseCors(this.CorsPolicy);

        base.WebApplication.UseSwaggerForOcelotUI(opt => opt.PathToSwaggerGenerator = "/swagger/docs").UseOcelot().Wait();
    }

    /// <inheritdoc/>
    protected override void ConfigureOptions()
    {
        base.ConfigureOption<HealthCheckSettings>();
    }

    /// <inheritdoc/>
    protected override void ConfigureMappers() { }
    /// <inheritdoc/>
    protected override void ConfigureRepositories() { }
    /// <inheritdoc/>
    protected override void ConfigureDataStores() { }
    /// <inheritdoc/>
    protected override void ConfigureBusinessServices() { }

    /// Does nothing because this application does not need seed data.
    protected override Task SeedData(IServiceScope serviceScope) 
    {
        return Task.CompletedTask;
    }
}