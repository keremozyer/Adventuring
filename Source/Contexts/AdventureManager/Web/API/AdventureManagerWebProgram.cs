using Adventuring.Architecture.Application.Web.Core;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.AdventureManager.Concern.Option;
using Adventuring.Contexts.AdventureManager.Concern.Option.Adventure;
using Adventuring.Contexts.AdventureManager.Data.Implementation.Adventure;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Context;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Repositories;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.Mappers.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Entity._Game;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;

namespace Adventuring.Contexts.AdventureManager.Web.API;

/// <summary>
/// Web program initializer.
/// </summary>
public class AdventureManagerWebProgram : BaseWebProgram
{
    /// <summary></summary>
    /// <param name="arguments">Application start arguments.</param>
    public AdventureManagerWebProgram(string[] arguments) : base(GetConfigurationOptions(arguments)) { }

    private static WebProgramConfigurationOptions GetConfigurationOptions(string[] arguments)
    {
        return new WebProgramConfigurationOptions(arguments)
        {
            AutoMapperProfile = new AdventureManagerAutoMapperProfile()
        };
    }

    /// <inheritdoc/>
    protected override void ConfigureOptions()
    {
        base.ConfigureOption<MongoSettings>();
        base.ConfigureOption<AdventureSettings>();
    }

    /// <inheritdoc/>
    protected override void ConfigureMappers()
    {
        base.ConfigureMapper<IAdventureMapper, AdventureMapper>();
        base.ConfigureMapper<IGameMapper, GameMapper>();
    }

    /// <inheritdoc/>
    protected override void ConfigureRepositories()
    {
        base.ConfigureRepository<IRepository<AdventureTree>, AdventureTreeRepository>();
        base.ConfigureRepository<IRepository<Game>, GameRepository>();
    }

    /// <inheritdoc/>
    protected override void ConfigureDataStores()
    {
        base.ConfigureDataStore<IAdventureDataStore, AdventureDataStore>();
        base.ConfigureDataStore<IGameDataStore, GameDataStore>();
    }

    /// <inheritdoc/>
    protected override void ConfigureBusinessServices()
    {
        base.ConfigureBusinessService<IAdventureTreeService, AdventureTreeService>();
        base.ConfigureBusinessService<IGameService, GameService>();
    }

    /// <inheritdoc/>
    protected override void ConfigureServices()
    {
        base.ConfigureNoSQLDatabase<AdventureManagerMongoClientHandler, AdventureManagerDataContext>();
    }

    /// <inheritdoc/>
    protected override void ConfigureWebApplication()
    {
        
    }

    /// <summary>
    /// Does nothing since this service does not requires seed data.
    /// </summary>
    /// <param name="serviceScope"></param>
    /// <returns></returns>
    protected override Task SeedData(IServiceScope serviceScope)
    {
        return Task.CompletedTask;
    }
}