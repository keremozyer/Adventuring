using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.List;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;
using AutoMapper;

namespace Adventuring.Contexts.AdventureManager.Mapper.Implementation.AutoMapperProfiles;

/// <summary>
/// AutoMapper profile for the application.
/// </summary>
public class AdventureManagerAutoMapperProfile : Profile
{
    /// <summary></summary>
    public AdventureManagerAutoMapperProfile()
    {
        CreateAdventureMaps();
        CreateGameMaps();
    }

    private void CreateAdventureMaps()
    {
        _ = CreateMap<AdventureNode, Model.DataTransferObject.AdventureTree.AdventureNode>()
            .ReverseMap();
        _ = CreateMap<Model.Domain.AdventureAggregate.AdventureNode, Model.DataTransferObject.AdventureTree.AdventureNode>();
        _ = CreateMap<Model.Contract.Adventure.Create.CreateAdventureRequestModel, CreateAdventureInputModel>();
        _ = CreateMap<CreateAdventureOutputModel, Model.Contract.Adventure.Create.CreateAdventureResponseModel>();
        _ = CreateMap<Model.Domain.AdventureAggregate.AdventureTree, CreateAdventureOutputModel>();
        _ = CreateMap<Model.Domain.AdventureAggregate.AdventureNode, Model.Entity.Adventure.AdventureNode>()
            .ForMember(entity => entity.ID, option => option.Ignore());
        _ = CreateMap<Model.Entity.Adventure.AdventureNode, Model.Domain.AdventureAggregate.AdventureNode>();
        _ = CreateMap<Model.Domain.AdventureAggregate.AdventureTree, Model.Entity.Adventure.AdventureTree>()
            .ForMember(entity => entity.ID, option => option.Ignore());
        _ = CreateMap<Model.Entity.Adventure.AdventureTree, Model.Domain.AdventureAggregate.AdventureTree>()
            .ConstructUsing(entity => new Model.Domain.AdventureAggregate.AdventureTree(entity.ID, entity.AdventureName, entity.StartingNode.NodeMessage));
        _ = CreateMap<Model.Domain.AdventureAggregate.AdventureTree, GetAdventureOutputModel>();
        _ = CreateMap<ListAdventuresOutputModel, ListAdventuresResponseModel>();
        _ = CreateMap<AdventureModel, AdventureResponseModel>();
        _ = CreateMap<GetAdventureOutputModel, GetAdventureResponseModel>();
    }

    private void CreateGameMaps()
    {
        _ = CreateMap<StartGameRequestModel, StartGameInputModel>();
        _ = CreateMap<StartGameOutputModel, StartGameResponseModel>();
        _ = CreateMap<Model.Domain.GameAggregate.Game, Model.Entity._Game.Game>()
            .ForMember(entity => entity.Answers, config => config.MapFrom(domain => domain.GetAnswers().Select(answer => answer.ChoosenPath).ToList()))
            .ForMember(entity => entity.ID, option => option.Ignore());
        _ = CreateMap<Model.Entity._Game.Game, Model.Domain.GameAggregate.Game>()
            .ConstructUsing(entity => new Model.Domain.GameAggregate.Game(entity.ID, entity.PlayerName, entity.AdventureID, entity.Answers, entity.IsGameOver));
        _ = CreateMap<Model.Domain.GameAggregate.Game, StartGameOutputModel>();
        _ = CreateMap<Model.Domain.GameAggregate.Game, GameModel>();
        _ = CreateMap<GameModel, GameResponseModel>();
        _ = CreateMap<ListGamesOutputModel, ListGamesResponseModel>();
        _ = CreateMap<PreviousNode, PreviouNodeResponseModel>();
        _ = CreateMap<GetGameOutputModel, GetGameResponseModel>();
        _ = CreateMap<AdvanceGameRequestModel, AdvanceGameInputModel>();
        _ = CreateMap<AdvanceGameOutputModel, AdvanceGameResponseModel>();
    }
}