using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;

namespace Adventuring.Contexts.AdventureManager.Data.Implementation.Adventure;

/// <inheritdoc/>
public class AdventureDataStore : IAdventureDataStore
{
    private readonly IRepository<Model.Entity.Adventure.AdventureTree> AdventureTreeRepository;
    private readonly IAdventureMapper AdventureMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="adventureTreeRepository"></param>
    /// <param name="adventureMapper"></param>
    public AdventureDataStore(IRepository<Model.Entity.Adventure.AdventureTree> adventureTreeRepository, IAdventureMapper adventureMapper)
    {
        this.AdventureTreeRepository = adventureTreeRepository;
        this.AdventureMapper = adventureMapper;
    }

    /// <inheritdoc/>
    public async Task<string> Create(Model.Domain.AdventureAggregate.AdventureTree adventureTree)
    {
        Model.Entity.Adventure.AdventureTree adventureTreeEntity = this.AdventureMapper.MapAdventureTree(adventureTree);

        return await this.AdventureTreeRepository.Create(adventureTreeEntity);
    }

    /// <inheritdoc/>
    public async Task<Model.Domain.AdventureAggregate.AdventureTree?> Get(string ID)
    {
        Model.Entity.Adventure.AdventureTree? adventureTreeEntity = await this.AdventureTreeRepository.Get(adventure => adventure.ID == ID);

        return this.AdventureMapper.MapAdventureTree(adventureTreeEntity);
    }

    /// <inheritdoc/>
    public async Task<Model.Domain.AdventureAggregate.AdventureTree?> GetByName(string adventureName)
    {
        Model.Entity.Adventure.AdventureTree? adventureTreeEntity = await this.AdventureTreeRepository.Get(adventure => adventure.AdventureName == adventureName);

        return this.AdventureMapper.MapAdventureTree(adventureTreeEntity);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Model.Domain.AdventureAggregate.AdventureTree>?> List()
    {
        IReadOnlyCollection<Model.Entity.Adventure.AdventureTree>? treeEntities = await this.AdventureTreeRepository.List();

        return this.AdventureMapper.Map(treeEntities);
    }
}