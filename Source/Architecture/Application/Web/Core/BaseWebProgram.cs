using Adventuring.Architecture.AppException.Manager.Implementation.FromConfiguration;
using Adventuring.Architecture.AppException.Manager.Interface;
using Adventuring.Architecture.Application.Web.Concern.Filter.StartupFilters;
using Adventuring.Architecture.Application.Web.Concern.Option.Database;
using Adventuring.Architecture.Application.Web.Concern.Option.OpenAPI;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Concern.Option.Auth;
using Adventuring.Architecture.Container.ActiveUser.Implementation.FromHttpContext;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Architecture.Data.Context.Interface;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Architecture.Model.Interface.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Adventuring.Architecture.Application.Web.Core;

/// <summary>
/// All web program initializers must be derived from this class to have common configurations.
/// </summary>
public abstract class BaseWebProgram
{
    /// <summary></summary>
    protected readonly WebApplicationBuilder WebApplicationBuilder;
    /// <summary></summary>
    protected readonly WebApplication WebApplication;

    private OpenAPISettings? OpenAPIValues;

    /// <summary>
    /// Will create a new <see cref="WebApplicationBuilder"/> and store it in the instance.
    /// After creation will initialize the builder with default service configurations and call <see cref="ConfigureServices"/> abstract method to perform application specific service configurations.
    /// Will build the <see cref="WebApplication"/> and store it in the instance.
    /// After building will initialize the application with default service configurations and call <see cref="ConfigureWebApplication"/> abstract method to perform application specific WebApplication configurations.
    /// </summary>
    /// <param name="webProgramConfigurationOptions"></param>
    public BaseWebProgram(WebProgramConfigurationOptions webProgramConfigurationOptions)
    {
        this.WebApplicationBuilder = WebApplication.CreateBuilder(webProgramConfigurationOptions.Arguments);

        InitializeWebApplicationBuilder(webProgramConfigurationOptions);

        this.WebApplication = this.WebApplicationBuilder.Build();

        InitializeWebApplication();
    }

    private void InitializeWebApplicationBuilder(WebProgramConfigurationOptions webProgramConfigurationOptions)
    {
        AddStandardConfigurationFiles();

        ApplyAuth();

        _ = this.WebApplicationBuilder.Services.AddHttpContextAccessor();

        this.OpenAPIValues = this.WebApplicationBuilder.Configuration.GetSection(nameof(OpenAPISettings)).Get<OpenAPISettings>();

        _ = this.WebApplicationBuilder.Services.AddControllers();
        _ = this.WebApplicationBuilder.Services
            .AddLogging()
            .AddHttpLogging(options => { });

        ConfigureAutoMapperProfile(webProgramConfigurationOptions.AutoMapperProfile);
        ConfigureOption<TokenSettings>();
        ConfigureOptions();

        _ = this.WebApplicationBuilder.Services.AddTransient<IStartupFilter, SettingValidationStartupFilter>();

        InitializeServices();

        _ = this.WebApplicationBuilder.Services.AddScoped<IActiveUser, ActiveHttpUser>();

        _ = this.WebApplicationBuilder.Services.AddHealthChecks();

        _ = this.WebApplicationBuilder.Services.AddSingleton<IExceptionParser, ExceptionParser>();

        ConfigureOpenAPI();
    }

    private void ApplyAuth()
    {
        TokenSettings? tokenSettings = this.WebApplicationBuilder.Configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();
        if (tokenSettings is null)
        {
            return;
        }

        _ = this.WebApplicationBuilder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SecurityKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };
            });
    }

    private void AddStandardConfigurationFiles()
    {
        string configurationsFolder = Path.Combine(AppContext.BaseDirectory, "Configurations");
        
        string[] configurationFilePaths = Directory.GetFiles(configurationsFolder, "*.json", SearchOption.AllDirectories);
        if (configurationFilePaths.IsNullOrEmpty())
        {
            return;
        }

        string environmentExtension = DetermineExtension(this.WebApplicationBuilder.Environment);

        List<string> validFilePaths = new();
        foreach (string configurationFilePath in configurationFilePaths)
        {
            // Some configurations are same across all environments so they may not have a file in the fileName.Environment.json format and just have a fileName.json format. To get those we check each file name to see if there is only one instance of it and if it's true we pick it regardless of the environment.
            string fileName = configurationFilePath.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
            validFilePaths.Add(configurationFilePaths.Count(path => path.Contains(fileName)) > 1 ? $"{configurationsFolder}{Path.DirectorySeparatorChar}{fileName}{environmentExtension}" : configurationFilePath);
        }

        foreach (string configurationFilePath in validFilePaths.Distinct())
        {
            _ = this.WebApplicationBuilder.Configuration.AddJsonFile(configurationFilePath, optional: false, reloadOnChange: true);
        }

        static string DetermineExtension(IWebHostEnvironment environment)
        {
            string? environmentPrefix = null;
            if (!environment.IsProduction())
            {
                environmentPrefix = $".{environment.EnvironmentName}";
            }

            return $"{environmentPrefix}.json";
        }
    }

    private void ConfigureAutoMapperProfile(Profile? autoMapperProfile)
    {
        if (autoMapperProfile is null)
        {
            return;
        }

        _ = this.WebApplicationBuilder.Services.AddSingleton(new MapperConfiguration(configuration => configuration.AddProfile(autoMapperProfile)).CreateMapper());
    }

    private void InitializeServices()
    {
        ConfigureMappers();
        ConfigureRepositories();
        ConfigureDataStores();
        ConfigureBusinessServices();
        ConfigureServices();
    }

    private void ConfigureOpenAPI()
    {
        if (this.OpenAPIValues is null)
        {
            return;
        }

        _ = this.WebApplicationBuilder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = this.OpenAPIValues.Title, Version = "v1" });
            foreach (string xmlCommentPath in Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly) ?? Array.Empty<string>())
            {
                options.IncludeXmlComments(xmlCommentPath);
            }

            if (!this.OpenAPIValues.HasSecurity!.Value)
            {
                return;
            }

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JUST PUT THE TOKEN ITSELF HERE, DO NOT APPEND PREFIXES LIKE 'BEARER '!"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    private void InitializeWebApplication()
    {
        _ = this.WebApplication
            .UseExceptionHandler("/Error")
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        ConfigureWebApplication();

        _ = this.WebApplication.MapHealthChecks("/health");

        ConfigureSwagger();
    }

    private void ConfigureSwagger()
    {
        if (this.OpenAPIValues is null)
        {
            return;
        }

        _ = this.WebApplication
            .UseSwagger()
            .UseSwaggerUI(config =>
            {
                foreach (EndpointSettings endpointSettings in this.OpenAPIValues.EndpointSettings)
                {
                    config.SwaggerEndpoint(endpointSettings.Endpoint, endpointSettings.APIName);
                }
            });
    }

    /// <summary>
    /// Will configure the given <typeparamref name="OptionType"/> with IOption pattern and hide IOption dependency behind a singleton service.
    /// If <typeparamref name="OptionType"/> implements <see cref="IValidatableSetting"/> it will be registered to the setting validation pipeline.
    /// </summary>
    /// <typeparam name="OptionType"></typeparam>
    protected void ConfigureOption<OptionType>() where OptionType : class, IOptionService
    {
        _ = this.WebApplicationBuilder.Services
            .Configure<OptionType>(this.WebApplicationBuilder.Configuration.GetSection(typeof(OptionType).Name))
            .AddSingleton(resolver => resolver.GetRequiredService<IOptions<OptionType>>().Value);

        if (typeof(IValidatableSetting).IsAssignableFrom(typeof(OptionType)))
        {
            _ = this.WebApplicationBuilder.Services.AddSingleton<IValidatableSetting>(resolver => (IValidatableSetting)resolver.GetRequiredService<IOptions<OptionType>>().Value);
        }
    }

    /// <summary>
    /// Will configure the given <typeparamref name="InterfaceType"/> and <typeparamref name="ImplementationType"/> as a singleton service.
    /// </summary>
    /// <typeparam name="InterfaceType"></typeparam>
    /// <typeparam name="ImplementationType"></typeparam>
    protected void ConfigureMapper<InterfaceType, ImplementationType>() where InterfaceType : class, IMapperService where ImplementationType : class, InterfaceType
    {
        _ = this.WebApplicationBuilder.Services.AddSingleton<InterfaceType, ImplementationType>();
    }

    /// <summary>
    /// Will configure the given <typeparamref name="InterfaceType"/> and <typeparamref name="ImplementationType"/> as a scoped service.
    /// </summary>
    /// <typeparam name="InterfaceType"></typeparam>
    /// <typeparam name="ImplementationType"></typeparam>
    protected void ConfigureRepository<InterfaceType, ImplementationType>() where InterfaceType : class, IRepositoryService where ImplementationType : class, InterfaceType
    {
        _ = this.WebApplicationBuilder.Services.AddScoped<InterfaceType, ImplementationType>();
    }

    /// <summary>
    /// Will configure the given <typeparamref name="InterfaceType"/> and <typeparamref name="ImplementationType"/> as a scoped service.
    /// </summary>
    /// <typeparam name="InterfaceType"></typeparam>
    /// <typeparam name="ImplementationType"></typeparam>
    protected void ConfigureDataStore<InterfaceType, ImplementationType>() where InterfaceType : class, IDataStoreService where ImplementationType : class, InterfaceType
    {
        _ = this.WebApplicationBuilder.Services.AddScoped<InterfaceType, ImplementationType>();
    }

    /// <summary>
    /// Will configure the given <typeparamref name="InterfaceType"/> and <typeparamref name="ImplementationType"/> as a scoped service.
    /// </summary>
    /// <typeparam name="InterfaceType"></typeparam>
    /// <typeparam name="ImplementationType"></typeparam>
    protected void ConfigureBusinessService<InterfaceType, ImplementationType>() where InterfaceType : class, IBusinessService where ImplementationType : class, InterfaceType
    {
        _ = this.WebApplicationBuilder.Services.AddScoped<InterfaceType, ImplementationType>();
    }

    /// <summary>
    /// Will configure <typeparamref name="DataContextType"/> as an SQL database using EntityFramework.
    /// </summary>
    /// <typeparam name="DataContextType"></typeparam>
    protected void ConfigureSQLDatabase<DataContextType>() where DataContextType : BaseDataContext
    {
        _ = this.WebApplicationBuilder.Services.AddDbContext<DataContextType>(options =>
        {
            _ = options.UseNpgsql(this.WebApplicationBuilder.Configuration.GetConnectionString(typeof(DataContextType).Name), postgreOptions => postgreOptions.UseRelationalNulls(true));
            _ = options
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(this.WebApplicationBuilder.Environment.IsDevelopment())
                .LogTo(Console.WriteLine);
        });

        _ = this.WebApplicationBuilder.Services.AddScoped<IDataContext>(resolver => resolver.GetRequiredService<DataContextType>());
    }

    /// <summary>
    /// Will configure <typeparamref name="ClientHandlerType"/> as a singleton service to handle MongoDB connections.
    /// Will configure <typeparamref name="DataContextType"/> as a scoped service to handle MongoDB transactions.
    /// </summary>
    /// <typeparam name="ClientHandlerType"></typeparam>
    /// <typeparam name="DataContextType"></typeparam>
    protected void ConfigureNoSQLDatabase<ClientHandlerType, DataContextType>() where ClientHandlerType : class where DataContextType : class
    {
        _ = this.WebApplicationBuilder.Services
                .AddSingleton<ClientHandlerType>()
                .AddScoped<DataContextType>();
    }

    /// <summary>
    /// Will migrate the <typeparamref name="DataContextType"/> to the latest migration and call <see cref="SeedData(IServiceScope)"/> abstract method to populate the database with startup values.
    /// </summary>
    /// <typeparam name="DataContextType"></typeparam>
    /// <returns></returns>
    protected async Task CheckDatabase<DataContextType>() where DataContextType : BaseDataContext
    {
        DatabaseConnectionRetrySettings databaseConnectionRetrySettings = this.WebApplication.Configuration.GetSection(nameof(DatabaseConnectionRetrySettings)).Get<DatabaseConnectionRetrySettings>() ?? new DatabaseConnectionRetrySettings();

        using IServiceScope serviceScope = this.WebApplication.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        int currentRunNumber = 0;
        do
        {
            try
            {
                currentRunNumber++;
                await serviceScope.ServiceProvider.GetRequiredService<DataContextType>().Database.MigrateAsync();
                break;
            }
            catch (Exception)
            {
                if (currentRunNumber < databaseConnectionRetrySettings.Retries.GetValueOrDefault())
                {
                    Thread.Sleep(databaseConnectionRetrySettings.IntervalInMilliseconds!.Value);
                }
            }
        } while (currentRunNumber <= databaseConnectionRetrySettings.Retries.GetValueOrDefault());

        await SeedData(serviceScope);
    }

    /// <summary>
    /// If implemented in an application will be used to configure option classes.
    /// </summary>
    protected abstract void ConfigureOptions();
    /// <summary>
    /// If implemented in an application will be used to configure mapper classes.
    /// </summary>
    protected abstract void ConfigureMappers();
    /// <summary>
    /// If implemented in an application will be used to configure repository classes.
    /// </summary>
    protected abstract void ConfigureRepositories();
    /// <summary>
    /// If implemented in an application will be used to configure data store classes.
    /// </summary>
    protected abstract void ConfigureDataStores();
    /// <summary>
    /// If implemented in an application will be used to configure business service classes.
    /// </summary>
    protected abstract void ConfigureBusinessServices();
    /// <summary>
    /// If implemented in an application will be used to configure services specific to that application.
    /// </summary>
    protected abstract void ConfigureServices();
    /// <summary>
    /// If implemented in an application will be used to configure WebApplication configurations specific to that application.
    /// </summary>
    protected abstract void ConfigureWebApplication();
    /// <summary>
    /// If implemented in an application will be used to seed database with initial values.
    /// </summary>
    protected abstract Task SeedData(IServiceScope serviceScope);

    /// <summary>
    /// Starts the web application.
    /// </summary>
    /// <returns></returns>
    public Task Run()
    {
        return this.WebApplication.RunAsync();
    }
}