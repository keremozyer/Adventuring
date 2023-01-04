using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Create;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;
using AutoMapper;

namespace Adventuring.Contexts.AdventureManager.Mapper.Implementation.Mappers.Adventure;

/// <inheritdoc/>
public class AdventureMapper : IAdventureMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public AdventureMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public CreateAdventureInputModel Map(CreateAdventureRequestModel model)
    {
        return this.Mapper.Map<CreateAdventureInputModel>(model);
    }

    /// <inheritdoc/>
    public CreateAdventureResponseModel Map(CreateAdventureOutputModel model)
    {
        return this.Mapper.Map<CreateAdventureResponseModel>(model);
    }

    /// <inheritdoc/>
    public CreateAdventureOutputModel Map(Model.Domain.AdventureAggregate.AdventureTree model)
    {
        return this.Mapper.Map<CreateAdventureOutputModel>(model);
    }

    /// <inheritdoc/>
    public Model.Entity.Adventure.AdventureTree MapAdventureTree(Model.Domain.AdventureAggregate.AdventureTree model)
    {
        return this.Mapper.Map<Model.Entity.Adventure.AdventureTree>(model);
    }

    /// <inheritdoc/>
    public Model.Domain.AdventureAggregate.AdventureTree? MapAdventureTree(Model.Entity.Adventure.AdventureTree? model)
    {
        Model.Domain.AdventureAggregate.AdventureTree? tree = this.Mapper.Map<Model.Domain.AdventureAggregate.AdventureTree>(model);
        if (tree is not null && model is not null)
        {
            tree.StartingNode = this.Mapper.Map<Model.Domain.AdventureAggregate.AdventureNode>(model.StartingNode);
        }

        return tree;
    }

    /// <inheritdoc/>
    public GetAdventureOutputModel MapGetAdventureOutputModel(Model.Domain.AdventureAggregate.AdventureTree model)
    {
        return this.Mapper.Map<GetAdventureOutputModel>(model);
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<Model.Domain.AdventureAggregate.AdventureTree>? Map(IReadOnlyCollection<Model.Entity.Adventure.AdventureTree>? model)
    {
        return model?.Select(MapAdventureTree)?.ToList()!;
    }

    /// <inheritdoc/>
    public ListAdventuresOutputModel Map(IReadOnlyCollection<Model.Domain.AdventureAggregate.AdventureTree>? adventures)
    {
        return new ListAdventuresOutputModel()
        {
            Adventures = adventures?.Select(adventure => new AdventureModel(adventure.ID, adventure.AdventureName, adventure.StartingNode.NodeMessage))?.ToArray()
        };
    }

    /// <inheritdoc/>
    public ListAdventuresResponseModel Map(ListAdventuresOutputModel model)
    {
        return this.Mapper.Map<ListAdventuresResponseModel>(model);
    }

    /// <inheritdoc/>
    public GetAdventureResponseModel Map(GetAdventureOutputModel model)
    {
        return this.Mapper.Map<GetAdventureResponseModel>(model);
    }
}