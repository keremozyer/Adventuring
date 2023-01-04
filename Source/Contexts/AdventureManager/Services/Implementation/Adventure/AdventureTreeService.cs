using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.AdventureManager.Concern.Option.Adventure;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;

namespace Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;

/// <inheritdoc/>
public class AdventureTreeService : IAdventureTreeService
{
    private readonly IAdventureMapper AdventureMapper;
    private readonly IAdventureDataStore AdventureDataStore;
    private readonly IActiveUser ActiveUser;
    private readonly AdventureSettings AdventureSettings;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="adventureMapper"></param>
    /// <param name="adventureDataStore"></param>
    /// <param name="activeUser"></param>
    /// <param name="adventureSettings"></param>
    public AdventureTreeService(IAdventureMapper adventureMapper, IAdventureDataStore adventureDataStore, IActiveUser activeUser, AdventureSettings adventureSettings)
    {
        this.AdventureMapper = adventureMapper;
        this.AdventureDataStore = adventureDataStore;
        this.ActiveUser = activeUser;
        this.AdventureSettings = adventureSettings;
    }

    /// <inheritdoc/>
    public async Task<CreateAdventureOutputModel> CreateAdventure(CreateAdventureInputModel input)
    {
        if (!(this.ActiveUser?.HasRole(this.AdventureSettings.AdventureCreatorRole)).GetValueOrDefault())
        {
            throw new UnauthorizedOperationException(nameof(CreateAdventure));
        }

        ValidateInput(input);

        AdventureTree? tree = await this.AdventureDataStore.GetByName(input.AdventureName);

        if (tree is not null)
        {
            throw new BusinessException(ErrorCodes.AdventureAlreadyExists);
        }

        tree = CreateTree(input);

        tree.ID = await this.AdventureDataStore.Create(tree);

        return this.AdventureMapper.Map(tree);

        static AdventureTree CreateTree(CreateAdventureInputModel input)
        {
            AdventureTree tree = new(input.AdventureName, input.StartingNode.NodeMessage);

            Traverse(tree.StartingNode, input.StartingNode); // We don't do a as-is mapping from DTO Node to domain Node and traverse the tree recursively to append nodes one by one because there could be domain rules when assigning nodes to the tree.

            ValidationException? validationResult = tree.Validate(); // We validate the tree again after traversing is complete.
            return validationResult is not null ? throw validationResult : tree;

            static void Traverse(Model.Domain.AdventureAggregate.AdventureNode domainNode, Model.DataTransferObject.AdventureTree.AdventureNode? inputNode)
            {
                if (inputNode?.PositiveAnswerNode is not null)
                {
                    domainNode.AddPositiveAnswerNode(inputNode.PositiveAnswerNode.NodeMessage);
                    Traverse(domainNode.PositiveAnswerNode!, inputNode.PositiveAnswerNode);
                }

                if (inputNode?.NegativeAnswerNode is not null)
                {
                    domainNode.AddNegativeAnswerNode(inputNode.NegativeAnswerNode.NodeMessage);
                    Traverse(domainNode.NegativeAnswerNode!, inputNode.NegativeAnswerNode);
                }
            }
        }

        static void ValidateInput(CreateAdventureInputModel input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(CreateAdventureInputModel));
            }

            if (input.StartingNode is null)
            {
                throw new ArgumentNullException(nameof(CreateAdventureInputModel.StartingNode));
            }
        }
    }

    /// <inheritdoc/>
    public async Task<GetAdventureOutputModel> Get(string ID)
    {
        AdventureTree tree = await this.AdventureDataStore.Get(ID) ?? throw new DataNotFoundException(nameof(AdventureTree), ID);
        return this.AdventureMapper.MapGetAdventureOutputModel(tree);
    }

    /// <inheritdoc/>
    public async Task<ListAdventuresOutputModel> List()
    {
        IReadOnlyCollection<AdventureTree>? adventures = await this.AdventureDataStore.List();

        return this.AdventureMapper.Map(adventures);
    }

    /// <inheritdoc/>
    public async Task<(string Message, bool IsEndNode)> GetTargetNodeMessage(string ID, IEnumerable<bool> answers)
    {
        AdventureTree tree = await this.AdventureDataStore.Get(ID) ?? throw new DataNotFoundException(nameof(AdventureTree), ID);

        return tree.GetTargetNodeMessage(answers);
    }
}