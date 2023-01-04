using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Create;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;

namespace Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;

/// <summary>
/// Maps related to the Adventure model.
/// </summary>
public interface IAdventureMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="CreateAdventureRequestModel"/> Contract to <see cref="CreateAdventureInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateAdventureInputModel Map(CreateAdventureRequestModel model);
    /// <summary>
    /// Maps given <see cref="CreateAdventureOutputModel"/> DTO to <see cref="CreateAdventureResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateAdventureResponseModel Map(CreateAdventureOutputModel model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Model to <see cref="CreateAdventureOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateAdventureOutputModel Map(Model.Domain.AdventureAggregate.AdventureTree model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Model to <see cref="Model.Entity.Adventure.AdventureTree"/> Entity.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Entity.Adventure.AdventureTree MapAdventureTree(Model.Domain.AdventureAggregate.AdventureTree model);
    /// <summary>
    /// Maps given <see cref="Model.Entity.Adventure.AdventureTree"/> Entity to <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Domain.AdventureAggregate.AdventureTree? MapAdventureTree(Model.Entity.Adventure.AdventureTree? model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Model to <see cref="GetAdventureOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetAdventureOutputModel MapGetAdventureOutputModel(Model.Domain.AdventureAggregate.AdventureTree model);
    /// <summary>
    /// Maps given <see cref="Model.Entity.Adventure.AdventureTree"/> Entities to <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Models.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public IReadOnlyCollection<Model.Domain.AdventureAggregate.AdventureTree>? Map(IReadOnlyCollection<Model.Entity.Adventure.AdventureTree>? model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.AdventureAggregate.AdventureTree"/> Domain Models to <see cref="ListAdventuresOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ListAdventuresOutputModel Map(IReadOnlyCollection<Model.Domain.AdventureAggregate.AdventureTree>? model);
    /// <summary>
    /// Maps given <see cref="ListAdventuresOutputModel"/> DTO to <see cref="ListAdventuresResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ListAdventuresResponseModel Map(ListAdventuresOutputModel model);
    /// <summary>
    /// Maps given <see cref="GetAdventureOutputModel"/> DTO to <see cref="GetAdventureResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetAdventureResponseModel Map(GetAdventureOutputModel model);
}