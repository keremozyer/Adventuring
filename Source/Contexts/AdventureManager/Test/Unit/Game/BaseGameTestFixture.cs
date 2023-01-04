using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.AdventureManager.Mapper.Implementation.Mappers.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Test.Unit._Base;
using AutoMapper;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit._Game;

public abstract class BaseGameTestFixture : BaseAdventureManagerTestFixture
{
    protected readonly IGameMapper GameMapper = new GameMapper(new MapperConfiguration(configuration => configuration.AddProfile(new AdventureManagerAutoMapperProfile())).CreateMapper());

    protected const string ValidAdventureID = "aa";
    protected const string AdventureName = "bb";

    protected readonly GetAdventureOutputModel ValidAdventure = new()
    {
        AdventureName = AdventureName,
        ID = ValidAdventureID,
        StartingNode = new AdventureNode()
        {
            NodeMessage = "starting",
            NegativeAnswerNode = new AdventureNode()
            {
                NodeMessage = "starting->negative"
            },
            PositiveAnswerNode = new AdventureNode()
            {
                NodeMessage = "starting->positive",
                PositiveAnswerNode = new AdventureNode()
                {
                    NodeMessage = "starting->positive->positive",
                    PositiveAnswerNode = new AdventureNode()
                    { 
                        NodeMessage = "starting->positive->positive->positive"
                    },
                    NegativeAnswerNode = new AdventureNode()
                    {
                        NodeMessage = "starting->positive->positive->negative"
                    }
                },
                NegativeAnswerNode = new AdventureNode()
                {
                    NodeMessage = "starting->positive->negative"
                }
            }
        }
    };

    protected Mock<IAdventureTreeService> HappyPathAdventureTreeServiceMock;
    protected Mock<IActiveUser> HappyPathActiveUserMock;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        this.HappyPathAdventureTreeServiceMock = new();
        _ = this.HappyPathAdventureTreeServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(this.ValidAdventure));

        this.HappyPathActiveUserMock = new();
        _ = this.HappyPathActiveUserMock.Setup(x => x.HasRole("Player")).Returns(true);
        _ = this.HappyPathActiveUserMock.Setup(x => x.Username).Returns("PlayerUser");
    }
}