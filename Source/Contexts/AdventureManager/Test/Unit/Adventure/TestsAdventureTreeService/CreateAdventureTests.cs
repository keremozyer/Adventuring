using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;
using Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;
using Moq;

namespace Adventuring.Contexts.AdventureManager.Test.Unit.Adventure.TestsAdventureTreeService;

[TestFixture]
public class CreateAdventureTests : BaseAdventureTestFixture
{
    private readonly CreateAdventureInputModel ValidInput = new()
    {
        AdventureName = "Adventure",
        StartingNode = new Model.DataTransferObject.AdventureTree.AdventureNode
        {
            NodeMessage = "Starting Node",
            PositiveAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
            {
                NodeMessage = "Positive Node",
                PositiveAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
                {
                    NodeMessage = "Positive Node Level 2",
                    PositiveAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
                    {
                        NodeMessage = "Positive Node Level 3",
                    },
                    NegativeAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
                    {
                        NodeMessage = "Negative Node Level 3",
                    }
                },
                NegativeAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
                {
                    NodeMessage = "Negative Node Level 2",
                }
            },
            NegativeAnswerNode = new Model.DataTransferObject.AdventureTree.AdventureNode
            {
                NodeMessage = "Negative Node",
            }
        }
    };

    private readonly string IDOfNewAdventure = "newID";
    private string AdventureCreatorRole;
    private readonly string ArgumentNullExceptionMessageTemplate = "Value cannot be null. (Parameter '{0}')";

    private Mock<IAdventureDataStore> HappyPathAdventureDataStoreMock;
    private Mock<IActiveUser> HappyPathActiveUserMock;

    [OneTimeSetUp]
    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp();

        this.AdventureCreatorRole = base.AdventureSettings.AdventureCreatorRole;

        this.HappyPathAdventureDataStoreMock = new();
        _ = this.HappyPathAdventureDataStoreMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(Task.FromResult<AdventureTree>(null));
        _ = this.HappyPathAdventureDataStoreMock.Setup(x => x.Create(It.IsAny<AdventureTree>())).Returns(Task.FromResult(this.IDOfNewAdventure));

        this.HappyPathActiveUserMock = new();
        _ = this.HappyPathActiveUserMock.Setup(x => x.HasRole(this.AdventureCreatorRole)).Returns(true);
    }

    [Test]
    public async Task CreateAdventure_HappyPath()
    {
        AdventureTreeService adventureTreeService = new(base.AdventureMapper, this.HappyPathAdventureDataStoreMock.Object, this.HappyPathActiveUserMock.Object, base.AdventureSettings);
        CreateAdventureOutputModel output = await adventureTreeService.CreateAdventure(this.ValidInput);

        Assert.Multiple(() =>
        {
            Assert.That(output, Is.Not.Null);
            Assert.That(output.ID, Is.EqualTo(this.IDOfNewAdventure));
        });
    }

    [Test]
    public void CreateAdventure_InvalidRole()
    {
        Mock<IActiveUser> activeUserMock = new();
        _ = activeUserMock.Setup(x => x.HasRole(this.AdventureCreatorRole)).Returns(false);

        AdventureTreeService adventureTreeService = new(base.AdventureMapper, this.HappyPathAdventureDataStoreMock.Object, activeUserMock.Object, base.AdventureSettings);

        Assert.That(async () => await adventureTreeService.CreateAdventure(this.ValidInput), Throws.TypeOf<UnauthorizedOperationException>());
    }

    [Test]
    public void CreateAdventure_NullInput()
    {
        AdventureTreeService adventureTreeService = new(base.AdventureMapper, this.HappyPathAdventureDataStoreMock.Object, this.HappyPathActiveUserMock.Object, base.AdventureSettings);

        Assert.That(async () => await adventureTreeService.CreateAdventure(null),
            Throws.TypeOf<ArgumentNullException>().And.Message.EqualTo(String.Format(this.ArgumentNullExceptionMessageTemplate, nameof(CreateAdventureInputModel))));
    }

    [Test]
    public void CreateAdventure_NullStartingNode()
    {
        AdventureTreeService adventureTreeService = new(base.AdventureMapper, this.HappyPathAdventureDataStoreMock.Object, this.HappyPathActiveUserMock.Object, base.AdventureSettings);

        CreateAdventureInputModel invalidInput = this.ValidInput.DeepCopy();
        invalidInput.StartingNode = null;

        Assert.That(async () => await adventureTreeService.CreateAdventure(invalidInput),
            Throws.TypeOf<ArgumentNullException>().And.Message.EqualTo(String.Format(this.ArgumentNullExceptionMessageTemplate, nameof(CreateAdventureInputModel.StartingNode))));
    }

    [Test]
    public void CreateAdventure_ExistingAdventure()
    {
        Mock<IAdventureDataStore> adventureDataStoreMock = new();
        _ = adventureDataStoreMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(Task.FromResult(new AdventureTree("aa", "bb")));

        AdventureTreeService adventureTreeService = new(base.AdventureMapper, adventureDataStoreMock.Object, this.HappyPathActiveUserMock.Object, base.AdventureSettings);

        Assert.That(async () => await adventureTreeService.CreateAdventure(this.ValidInput),
            Throws.TypeOf<BusinessException>().And.Property(nameof(BusinessException.Messages)).One.Property(nameof(BaseAppExceptionMessage.Code)).EqualTo(ErrorCodes.AdventureAlreadyExists));
    }
}