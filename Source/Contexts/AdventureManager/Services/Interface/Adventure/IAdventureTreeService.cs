using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;

namespace Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;

/// <summary>
/// Business flows for adventure tree model.
/// </summary>
public interface IAdventureTreeService : IBusinessService
{
    /// <summary>
    /// Creates a new adventure with given parameters. Will throw a BusinessException with AdventureAlreadyExists error code if an adventure with given name already exists.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<CreateAdventureOutputModel> CreateAdventure(CreateAdventureInputModel input);
    /// <summary>
    /// Returns the adventure specified by this ID. Will throw a DataNotFoundException if an adventure is not found.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public Task<GetAdventureOutputModel> Get(string ID);
    /// <summary>
    /// Lists all adventures in the application. <see cref="ListAdventuresOutputModel"/> will not be <see langword="null"/> if no adventure is found but <see cref="ListAdventuresOutputModel.Adventures"/> will be <see langword="null"/>.
    /// </summary>
    /// <returns></returns>
    public Task<ListAdventuresOutputModel> List();
    /// <summary>
    /// Returns the target node's message and whether that node is an end node or not for the adventure specified in <paramref name="ID"/> parameter following the path given in <paramref name="answers"/> parameter.
    /// </summary>
    /// <param name="ID">ID of the adventure to travers.</param>
    /// <param name="answers">Path to take.</param>
    /// <returns></returns>
    public Task<(string Message, bool IsEndNode)> GetTargetNodeMessage(string ID, IEnumerable<bool> answers);
}